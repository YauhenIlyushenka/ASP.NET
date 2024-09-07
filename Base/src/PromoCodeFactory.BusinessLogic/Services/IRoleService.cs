using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IRoleService
	{
		Task<List<RoleItemResponseDto>> GetAllAsync();
	}
}
