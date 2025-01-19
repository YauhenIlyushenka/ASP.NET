using Pcf.ReceivingFromPartner.Business.Models;
using Pcf.ReceivingFromPartner.Business.Models.Partner;

namespace Pcf.ReceivingFromPartner.Business.Services
{
	public interface IPartnerService
	{
		Task<List<PartnerDto>> GetAllAsync();
		Task<PartnerDto> GetByIdAsync(Guid id);
		Task<PartnerPromoCodeLimitDto> GetPartnerLimitAsync(Guid id, Guid limitId);
		Task<PartnerPromoCodeLimitDto> SetPartnerPromoCodeLimitAsync(Guid id, PartnerPromoCodeLimitRequestDto request);
		Task CancelPartnerPromoCodeLimitAsync(Guid id);
		Task<List<PromoCodeShortResponseDto>> GetPromoCodesByPartnerIdAsync(Guid id);
		Task<PromoCodeShortResponseDto> GetPartnerPromoCodeAsync(Guid partnerId, Guid promoCodeId);
		Task ReceivePromoCodeFromPartnerWithPreferenceAsync(Guid id, ReceivingPromoCodeRequestDto request);
	}
}
