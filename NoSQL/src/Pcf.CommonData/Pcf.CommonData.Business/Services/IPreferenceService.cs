using Pcf.CommonData.Business.Models;

namespace Pcf.CommonData.Business.Services
{
	public interface IPreferenceService
	{
		Task<List<PreferenceResponseDto>> GetAllAsync();
		Task<PreferenceResponseDto> GetPreferenceById(int id);
	}
}
