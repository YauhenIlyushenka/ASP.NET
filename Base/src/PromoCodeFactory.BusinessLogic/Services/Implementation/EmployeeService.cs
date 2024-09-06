using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee> _employeeRepository;
		private readonly IRepository<Role> _roleRepository;

		public EmployeeService(
			IRepository<Employee> employeeRepository,
			IRepository<Role> roleRepository)
		{
			_employeeRepository = employeeRepository;
			_roleRepository = roleRepository;
		}

		public async Task<List<EmployeeShortResponseDto>> GetAllAsync()
			=> (await _employeeRepository.GetAllAsync()).Select(x => new EmployeeShortResponseDto
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

		public async Task<EmployeeResponseDto> GetByIdAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id);

			if (employee == null)
			{
				throw new Exception();
			}

			return new EmployeeResponseDto
			{
				Id = employee.Id,
				Email = employee.Email,
				Roles = employee.Roles.Select(x => new RoleItemResponseDto
				{
					Id = employee.Id,
					Name = x.Name,
					Description = x.Description
				}).ToList(),
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		public async Task CreateAsync(EmpoyeeRequestDto model)
		{
			var role = (await _roleRepository.GetAllAsync()).SingleOrDefault(role => role.Id == model.RoleId);

			await _employeeRepository.CreateAsync(new Employee
			{
				Id = Guid.NewGuid(),
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Roles = new List<Role> { role }
			});
		}

		public async Task UpdateAsync(EmployeeRequestExtendedDto model)
		{
			var employee = await _employeeRepository.GetByIdAsync(model.Id);

			if (employee == null)
			{
				throw new Exception();
			}

			var roles = (await _roleRepository.GetAllAsync())
				.Where(role => model.RoleIds.Contains(role.Id))
				.ToList();

			employee.FirstName = model.FirstName;
			employee.LastName = model.LastName;
			employee.Email = model.Email;
			employee.AppliedPromocodesCount = model.AppliedPromocodesCount;
			employee.Roles = roles;
		}

		public async Task DeleteAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id);

			if (employee == null)
			{
				throw new Exception();
			}

			await _employeeRepository.DeleteAsync(employee);
		}
	}
}