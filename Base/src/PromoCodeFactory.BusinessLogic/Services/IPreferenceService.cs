using PromoCodeFactory.BusinessLogic.Models.Preference;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IPreferenceService
	{
		Task<List<PreferenceResponseDto>> GetAllAsync();
	}
}
