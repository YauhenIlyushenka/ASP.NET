using PromoCodeFactory.BusinessLogic.Models.Employee;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IEmployeeService
	{
		Task<List<EmployeeShortResponseDto>> GetAllAsync();

		Task<EmployeeResponseDto> GetByIdAsync(Guid id);

		Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto model);

		Task UpdateAsync(Guid id, EmployeeRequestDto model);

		Task DeleteAsync(Guid id);
	}
}
