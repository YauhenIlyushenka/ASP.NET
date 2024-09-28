using PromoCodeFactory.BusinessLogic.Models.Partner;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IPartnerService
	{
		Task<List<PartnerDto>> GetAllAsync();
		Task<PartnerPromoCodeLimitDto> GetPartnerLimitAsync(Guid id, Guid limitId);
		Task<PartnerPromoCodeLimit> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request);
		Task CancelPartnerPromoCodeLimitAsync(Guid id);
	}
}