using Pcf.ReceivingFromPartner.Business.Models;

namespace Pcf.ReceivingFromPartner.Business.Services
{
	public interface IPreferenceService
	{
		Task<List<PreferenceResponseDto>> GetAllAsync();
		Task<PreferenceResponseDto> GetPreferenceByIdAsync(int id);
	}
}
