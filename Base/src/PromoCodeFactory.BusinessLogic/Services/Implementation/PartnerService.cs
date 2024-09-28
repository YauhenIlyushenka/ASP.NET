using PromoCodeFactory.BusinessLogic.Models.Partner;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PartnerService : BaseService, IPartnerService
	{
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
					IsActive = true,
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
		//public async Task<PartnerPromoCodeLimit> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request)
		//{
		//	var partner = await _partnerRepository.GetByIdAsync(x => x.Id.Equals(id), includes: nameof(Partner.PartnerLimits))
		//		?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Partner)));

		//	if (!partner.IsActive) 
		//		throw new BadRequestException($" The {nameof(Partner)} with id: {partner.Id} is not active.");



		//}

		//public Task CancelPartnerPromoCodeLimitAsync(Guid id)
		//{
		//	throw new NotImplementedException();
		//}

	}
}
