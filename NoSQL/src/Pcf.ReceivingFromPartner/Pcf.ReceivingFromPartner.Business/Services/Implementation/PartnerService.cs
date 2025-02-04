using Pcf.ReceivingFromPartner.Business.Models;
using Pcf.ReceivingFromPartner.Business.Models.Partner;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Abstractions.Repositories;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.Core.Exceptions;
using Pcf.ReceivingFromPartner.Core.Helpers;

namespace Pcf.ReceivingFromPartner.Business.Services.Implementation
{
	public class PartnerService : BaseService, IPartnerService
	{
		private const int DefaultCountIssuedPromoCodes = 0;

		private readonly IRepository<Partner, Guid> _partnerRepository;
		private readonly IPreferenceService _preferenceService;

		private readonly INotificationGateway _notificationGateway;
		private readonly IRabbitMQGateway _rabbitMQGateway;
		//private readonly IGivingPromoCodeToCustomerGateway _givingPromoCodeToCustomerGateway;
		//private readonly IAdministrationGateway _administrationGateway;

		public PartnerService(
			IRepository<Partner, Guid> partnerRepository,
			IPreferenceService preferenceService,
			INotificationGateway notificationGateway,
			IRabbitMQGateway rabbitMQGateway
			//IGivingPromoCodeToCustomerGateway givingPromoCodeToCustomerGateway,
			//IAdministrationGateway administrationGateway
			)
		{
			_partnerRepository = partnerRepository;
			_preferenceService = preferenceService;
			_notificationGateway = notificationGateway;
			_rabbitMQGateway = rabbitMQGateway;
			//_givingPromoCodeToCustomerGateway = givingPromoCodeToCustomerGateway;
			//_administrationGateway = administrationGateway;
		}

		public async Task<List<PartnerDto>> GetAllAsync()
			=> (await _partnerRepository.GetAllAsync(includes: nameof(Partner.PartnerLimits), asNoTracking: true))
				.Select(partner => new PartnerDto
				{
					Id = partner.Id,
					Name = partner.Name,
					NumberIssuedPromoCodes = partner.NumberIssuedPromoCodes,
					IsActive = partner.IsActive,
					PartnerLimits = partner.PartnerLimits.Select(limit => new PartnerPromoCodeLimitDto()
					{
						Id = limit.Id,
						PartnerId = limit.PartnerId,
						Limit = limit.Limit,
						CreateDate = limit.CreateDate.ToDateString(),
						EndDate = limit.EndDate.ToDateString(),
						CancelDate = limit.CancelDate?.ToDateString() ?? string.Empty,
					}).ToList()
				}).ToList();

		public async Task<PartnerDto> GetByIdAsync(Guid id)
		{
			var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits), asNoTracking: true)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			return new PartnerDto
			{
				Id = partner.Id,
				Name = partner.Name,
				NumberIssuedPromoCodes = partner.NumberIssuedPromoCodes,
				IsActive = partner.IsActive,
				PartnerLimits = partner.PartnerLimits.Select(limit => new PartnerPromoCodeLimitDto()
				{
					Id = limit.Id,
					PartnerId = limit.PartnerId,
					Limit = limit.Limit,
					CreateDate = limit.CreateDate.ToDateString(),
					EndDate = limit.EndDate.ToDateString(),
					CancelDate = limit.CancelDate?.ToDateString() ?? string.Empty,
				}).ToList()
			};
		}

		public async Task<PartnerPromoCodeLimitDto> GetPartnerLimitAsync(Guid id, Guid limitId)
		{
			var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits), asNoTracking: true)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			var limit = partner.PartnerLimits.FirstOrDefault(x => x.Id.Equals(limitId))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(PartnerPromoCodeLimit)));

			return new PartnerPromoCodeLimitDto
			{
				Id = limit.Id,
				PartnerId = limit.PartnerId,
				Limit = limit.Limit,
				CreateDate = limit.CreateDate.ToDateString(),
				EndDate = limit.EndDate.ToDateString(),
				CancelDate = limit.CancelDate?.ToDateString() ?? string.Empty,
			};
		}

		public async Task<PartnerPromoCodeLimitDto> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request)
		{
			var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			if (!partner.IsActive)
				throw new BadRequestException($"The {nameof(Partner)} with id: {partner.Id} is not active.");

			var activeLimit = partner.PartnerLimits.FirstOrDefault(x => !x.CancelDate.HasValue && x.EndDate > DateTime.UtcNow);

			if (activeLimit != null)
			{
				partner.NumberIssuedPromoCodes = DefaultCountIssuedPromoCodes;
				activeLimit.CancelDate = DateTime.UtcNow;
			}

			var newLimit = new PartnerPromoCodeLimit
			{
				Limit = request.Limit,
				PartnerId = partner.Id,
				Partner = partner,
				CreateDate = DateTime.UtcNow,
				EndDate = request.EndDate.ToDateTime(),
			};

			partner.PartnerLimits.Add(newLimit);
			_partnerRepository.Update(partner);

			await _partnerRepository.SaveChangesAsync();
			await _notificationGateway.SendNotificationToPartnerAsync(
				partner.Id,
				"You have a limit on sending promocodes...");

			return new PartnerPromoCodeLimitDto
			{
				Id = newLimit.Id,
				PartnerId = partner.Id,
				Partner = new PartnerDto
				{
					Name = partner.Name,
					IsActive = partner.IsActive,
					NumberIssuedPromoCodes = partner.NumberIssuedPromoCodes,
					PartnerLimits = partner.PartnerLimits.Select(limit => new PartnerPromoCodeLimitDto()
					{
						Id = limit.Id,
						PartnerId = limit.PartnerId,
						Limit = limit.Limit,
						CreateDate = limit.CreateDate.ToDateString(),
						EndDate = limit.EndDate.ToDateString(),
						CancelDate = limit.CancelDate?.ToDateString() ?? string.Empty,
					}).ToList()
				},
				Limit = newLimit.Limit,
				CreateDate = newLimit.CreateDate.ToDateString(),
				EndDate = newLimit.EndDate.ToDateString(),
			};
		}

		public async Task CancelPartnerPromoCodeLimitAsync(Guid id)
		{
			var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			if (!partner.IsActive)
				throw new BadRequestException($"The {nameof(Partner)} with id: {partner.Id} is not active.");

			var activeLimit = partner.PartnerLimits.FirstOrDefault(x => !x.CancelDate.HasValue && x.EndDate > DateTime.UtcNow)
				?? throw new BadRequestException($"No active {nameof(Partner.PartnerLimits)} found for {nameof(Partner)} with id: {partner.Id}.");

			activeLimit.CancelDate = DateTime.UtcNow;
			_partnerRepository.Update(partner);
			await _partnerRepository.SaveChangesAsync();

			await _notificationGateway.SendNotificationToPartnerAsync(
				partner.Id,
				"Your limit on sending promocodes has been declined...");
		}

		public async Task<List<PromoCodeShortResponseDto>> GetPromoCodesByPartnerIdAsync(Guid id)
		{
			var partner = await _partnerRepository.GetByIdAsync(
				x => x.Id.Equals(id),
				includes: nameof(Partner.PromoCodes),
				asNoTracking: true)
					?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			return partner.PromoCodes.Select(x => new PromoCodeShortResponseDto
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate.ToDateString(),
				EndDate = x.EndDate.ToDateString(),
				PartnerId = x.PartnerId,
				PartnerName = x.Partner.Name,
				ServiceInfo = x.ServiceInfo,
			}).ToList();
		}

		public async Task<PromoCodeShortResponseDto> GetPartnerPromoCodeAsync(Guid partnerId, Guid promoCodeId)
		{
			var partner = await _partnerRepository.GetByIdAsync(
				x => x.Id.Equals(partnerId),
				includes: nameof(Partner.PromoCodes),
				asNoTracking: true)
					?? throw new NotFoundException(FormatFullNotFoundErrorMessage(partnerId, nameof(Partner)));

			var promoCode = partner.PromoCodes.FirstOrDefault(x => x.Id == promoCodeId)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(promoCodeId, nameof(PromoCode)));

			return new PromoCodeShortResponseDto
			{
				Id = promoCode.Id,
				Code = promoCode.Code,
				BeginDate = promoCode.BeginDate.ToDateString(),
				EndDate = promoCode.EndDate.ToDateString(),
				PartnerId = promoCode.PartnerId,
				PartnerName = promoCode.Partner.Name,
				ServiceInfo = promoCode.ServiceInfo,
			};
		}

		public async Task ReceivePromoCodeFromPartnerWithPreferenceAsync(Guid id, ReceivingPromoCodeRequestDto request)
		{
			var partner = await _partnerRepository.GetByIdAsync(
				x => x.Id.Equals(id),
				includes: $"{nameof(Partner.PartnerLimits)},{nameof(Partner.PromoCodes)}")
					?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			var activeLimit = partner.PartnerLimits.FirstOrDefault(x => !x.CancelDate.HasValue && x.EndDate > DateTime.UtcNow)
				?? throw new BadRequestException($"There is no limit available for providing promocodes.");

			if (partner.NumberIssuedPromoCodes + 1 > activeLimit.Limit)
				throw new BadRequestException("The limit for giving promocodes has been exceeded");
			
			if (partner.PromoCodes.Any(x => x.Code.Equals(request.PromoCode)))
				throw new BadRequestException("This promocode has already been given previously");

			var preference = await _preferenceService.GetPreferenceByIdAsync((int)request.Preference)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Preference)));

			var promoCode = new PromoCode
			{
				PartnerId = partner.Id,
				Partner = partner,
				Code = request.PromoCode,
				ServiceInfo = request.ServiceInfo,
				BeginDate = DateTime.UtcNow,
				EndDate = DateTime.UtcNow.AddDays(30),
				PreferenceId = preference.Id,
				PreferenceName = preference.Name,
				PartnerManagerId = request.PartnerManagerId,
			};

			partner.PromoCodes.Add(promoCode);
			partner.NumberIssuedPromoCodes++;

			_partnerRepository.Update(partner);
			await _partnerRepository.SaveChangesAsync();

			//TODO: Чтобы информация о том, что промокод был выдан партнером была отправлена
			//в микросервис рассылки клиентам нужно либо вызвать его API, либо отправить событие в очередь
			//await _givingPromoCodeToCustomerGateway.GivePromoCodeToCustomer(promoCode);

			//TODO: Чтобы информация о том, что промокод был выдан партнером была отправлена
			//в микросервис администрирования нужно либо вызвать его API, либо отправить событие в очередь
			//if (request.PartnerManagerId.HasValue)
			//{
			//	await _administrationGateway.NotifyAdminAboutPartnerManagerPromoCode(request.PartnerManagerId.Value);
			//}

			await _rabbitMQGateway.SendNotificationAboutGivingPromocode(promoCode);
		}
	}
}
