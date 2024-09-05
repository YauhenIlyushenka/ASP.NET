using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee> _employeeRepository;

		public EmployeeService(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		public async Task<List<EmployeeShortResponseDto>> GetAllAsync()
			=> (await _employeeRepository.GetAllAsync()).Select(x => new EmployeeShortResponseDto
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

		public async Task<EmployeeResponseDto?> GetByIdAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id);

			return employee != null
				? new EmployeeResponseDto
				{
					Id = employee.Id,
					Email = employee.Email,
					Roles = employee.Roles.Select(x => new RoleItemResponseDto
					{
						Name = x.Name,
						Description = x.Description
					}).ToList(),
					FullName = employee.FullName,
					AppliedPromocodesCount = employee.AppliedPromocodesCount
				}
				: null;
		}

	}
}