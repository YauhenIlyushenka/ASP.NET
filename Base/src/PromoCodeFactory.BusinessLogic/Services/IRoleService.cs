using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IRoleService
	{
		Task<List<RoleItemResponseDto>> GetAllAsync();

		Task<RoleItemResponseDto?> GetByIdAsync(Guid id);

		Task CreateAsync(RoleItemRequestDto model);

		Task UpdateAsync(RoleItemRequestDto model);

		Task DeleteAsync(Guid id);
	}
}
