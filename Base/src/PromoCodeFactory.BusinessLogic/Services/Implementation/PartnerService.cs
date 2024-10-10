using PromoCodeFactory.BusinessLogic.Models.Partner;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PartnerService : BaseService, IPartnerService
	{
		private const int DefaultCountIssuedPromoCodes = 0;
		private readonly IRepository<Partner, Guid> _partnerRepository;

		public PartnerService(IRepository<Partner, Guid> partnerRepository)
		{
			_partnerRepository = partnerRepository;
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
						CancelDate = limit.CancelDate?.ToDateString(),
					}).ToList()
				}).ToList();

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
				CancelDate = limit.CancelDate?.ToDateString(),
			};
		}

		public async Task<PartnerPromoCodeLimitDto> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request)
		{
			var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

			if (!partner.IsActive)
				throw new BadRequestException($"The {nameof(Partner)} with id: {partner.Id} is not active.");

			var activeLimit = partner.PartnerLimits.FirstOrDefault(x => !x.CancelDate.HasValue && x.EndDate > DateTime.Now);

			if (activeLimit != null)
			{
				partner.NumberIssuedPromoCodes = DefaultCountIssuedPromoCodes;
				activeLimit.CancelDate = DateTime.Now;
			}

			var newLimit = new PartnerPromoCodeLimit
			{
				Limit = request.Limit,
				CreateDate = DateTime.Now,
				EndDate = request.EndDate.ToDateTime(),
			};

			partner.PartnerLimits.Add(newLimit);
			_partnerRepository.Update(partner);

			await _partnerRepository.SaveChangesAsync();

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
						CancelDate = limit.CancelDate?.ToDateString(),
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

			var activeLimit = partner.PartnerLimits.FirstOrDefault(x => !x.CancelDate.HasValue && x.EndDate > DateTime.Now)
				?? throw new BadRequestException($"No active {nameof(Partner.PartnerLimits)} found for {nameof(Partner)} with id: {partner.Id}.");

			activeLimit.CancelDate = DateTime.Now;
			_partnerRepository.Update(partner);

			await _partnerRepository.SaveChangesAsync();
		}
	}
}
