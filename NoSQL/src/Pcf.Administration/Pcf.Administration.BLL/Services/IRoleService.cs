using Pcf.Administration.BLL.Models;

namespace Pcf.Administration.BLL.Services
{
	public interface IRoleService
	{
		Task<List<RoleItemResponseDto>> GetAllAsync();
	}
}
