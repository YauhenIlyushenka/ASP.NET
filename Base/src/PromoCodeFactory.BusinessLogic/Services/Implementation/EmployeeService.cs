using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Exceptions;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee, Guid> _employeeRepository;
		private readonly IRepository<Role, Guid> _roleRepository;

		public EmployeeService(
			IRepository<Employee, Guid> employeeRepository,
			IRepository<Role, Guid> roleRepository)
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
				Role = new RoleItemResponseDto
				{
					Id = employee.Role.Id,
					Name = employee.Role.Name,
					Description = employee.Role.Description,
				},
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto model)
		{
			var role = (await _roleRepository.GetAllAsync())
				.Single(role => role.Name.Equals(model.Role.ToString(), StringComparison.OrdinalIgnoreCase));

			var employee = await _employeeRepository.AddAsync(new Employee
			{
				Id = Guid.NewGuid(),
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = role,
			});

			await _employeeRepository.SaveChangesAsync();

			return new EmployeeResponseDto
			{
				Id = employee.Id,
				Email = employee.Email,
				Role = new RoleItemResponseDto
				{
					Id = employee.Role.Id,
					Name = employee.Role.Name,
					Description = employee.Role.Description,
				},
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		public async Task UpdateAsync(Guid id, EmployeeRequestDto model)
		{
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id));

			var role = (await _roleRepository.GetAllAsync())
				.Single(role => role.Name.Equals(model.Role.ToString(), StringComparison.OrdinalIgnoreCase));

			employee.FirstName = model.FirstName;
			employee.LastName = model.LastName;
			employee.Email = model.Email;
			employee.AppliedPromocodesCount = model.AppliedPromocodesCount;
			employee.Role = role;

			_employeeRepository.Update(employee);
			await _employeeRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id));

			_employeeRepository.Delete(employee);
			await _employeeRepository.SaveChangesAsync();
		}

		private string FormatFullNotFoundErrorMessage(Guid id) => $"The employee with Id {id} has not been found.";
	}
}