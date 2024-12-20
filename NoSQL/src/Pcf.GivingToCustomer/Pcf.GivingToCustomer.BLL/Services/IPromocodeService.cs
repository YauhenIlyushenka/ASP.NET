using Pcf.GivingToCustomer.BLL.Models;

namespace Pcf.GivingToCustomer.BLL.Services
{
	public interface IPromocodeService
	{
		Task<List<PromoCodeShortResponseDto>> GetAllAsync();
		Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequestDto request);
	}
}
