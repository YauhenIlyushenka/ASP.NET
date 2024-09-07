using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Exceptions;

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
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id));

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
			var role = (await _roleRepository.GetAllAsync())
				.Single(role => role.Name.Equals(model.Role.ToString(), StringComparison.OrdinalIgnoreCase));

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
			var employee = await _employeeRepository.GetByIdAsync(model.Id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(model.Id));

			var roles = (await _roleRepository.GetAllAsync())
				.Where(role => model.Roles.Select(x => x.ToString().ToLower()).Contains(role.Name.ToLower()))
				.ToList();

			employee.Update(
				model.FirstName,
				model.LastName,
				model.Email,
				model.AppliedPromocodesCount,
				roles);
		}

		public async Task DeleteAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id));

			await _employeeRepository.DeleteAsync(employee);
		}

		private string FormatFullNotFoundErrorMessage(Guid id) => $"The employee with Id {id} has not been found.";
	}
}