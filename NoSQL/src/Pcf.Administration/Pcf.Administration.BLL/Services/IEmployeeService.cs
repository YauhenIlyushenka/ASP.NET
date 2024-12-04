using Pcf.Administration.BLL.Models;

namespace Pcf.Administration.BLL.Services
{
	public interface IEmployeeService
	{
		Task<List<EmployeeShortResponseDto>> GetAllAsync();
		Task<EmployeeResponseDto> GetByIdAsync(Guid id);
		Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto model);
		Task UpdateAsync(Guid id, EmployeeRequestDto model);
		Task DeleteAsync(Guid id);
		Task UpdateAppliedPromocodesAsync(Guid id);
	}
}
