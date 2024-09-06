using PromoCodeFactory.BusinessLogic.Models.Employee;

namespace PromoCodeFactory.BusinessLogic.Services
{
	public interface IEmployeeService
	{
		Task<List<EmployeeShortResponseDto>> GetAllAsync();

		Task<EmployeeResponseDto> GetByIdAsync(Guid id);

		Task CreateAsync(EmpoyeeRequestDto model);

		Task UpdateAsync(EmployeeRequestExtendedDto model);

		Task DeleteAsync(Guid id);
	}
}
