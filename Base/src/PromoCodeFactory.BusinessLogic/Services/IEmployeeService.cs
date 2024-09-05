using PromoCodeFactory.BusinessLogic.Models.Employee;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IEmployeeService
	{
		Task<List<EmployeeShortResponseDto>> GetAllAsync();

		Task<EmployeeResponseDto?> GetByIdAsync(Guid id);

		//Task CreateEmployee(EmployeeCreateRequest EmployeeCreateRequest);

		//Task DeleteEmployeeAsync(Guid id);

		//Task UpdateEmployeeAsync(EmployeeRequest employee);
	}
}
