using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Exceptions;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class EmployeeService : BaseService, IEmployeeService
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
			=> (await _employeeRepository.GetAllAsync(asNoTracking: true)).Select(x => new EmployeeShortResponseDto
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

		public async Task<EmployeeResponseDto> GetByIdAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(x => x.Id.Equals(id), $"{nameof(Employee.Role)}", asNoTracking: true)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

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
			var role = (await _roleRepository
				.GetAllAsync(role => role.Name.Equals(model.Role.ToString())))
				.Single();

			var employee = await _employeeRepository.AddAsync(new Employee
			{
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
			var employee = await _employeeRepository.GetByIdAsync(x => x.Id.Equals(id))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

			var role = (await _roleRepository
				.GetAllAsync(role => role.Name.Equals(model.Role.ToString())))
				.Single();

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
			var employee = await _employeeRepository.GetByIdAsync(x => x.Id.Equals(id))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

			_employeeRepository.Delete(employee);
			await _employeeRepository.SaveChangesAsync();
		}
	}
}