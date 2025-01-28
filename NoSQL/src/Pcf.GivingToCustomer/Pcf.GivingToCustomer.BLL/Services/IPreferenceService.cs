using Pcf.GivingToCustomer.BLL.Models;

namespace Pcf.GivingToCustomer.BLL.Services
{
	public interface IPreferenceService
	{
		Task<List<PreferenceResponseDto>> GetAllAsync();
	}
}
