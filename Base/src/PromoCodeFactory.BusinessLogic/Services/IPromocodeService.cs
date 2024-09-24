using PromoCodeFactory.BusinessLogic.Models.PromoCode;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IPromocodeService
	{
		Task<List<PromoCodeShortResponseDto>> GetAllAsync();

		Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequestDto request);
	}
}
