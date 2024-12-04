using Pcf.Administration.BusinessLogic.Models;

namespace Pcf.Administration.BusinessLogic.Services
{
	public interface IRoleService
	{
		Task<List<RoleItemResponseDto>> GetAllAsync();
	}
}
