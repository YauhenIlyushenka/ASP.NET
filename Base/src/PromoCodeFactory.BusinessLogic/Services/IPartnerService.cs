using PromoCodeFactory.BusinessLogic.Models.Partner;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IPartnerService
	{
		Task<List<PartnerDto>> GetAllAsync();
		Task<PartnerPromoCodeLimitDto> GetPartnerLimitAsync(Guid id, Guid limitId);
		Task<PartnerPromoCodeLimitDto> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request);
		Task CancelPartnerPromoCodeLimitAsync(Guid id);
	}
}