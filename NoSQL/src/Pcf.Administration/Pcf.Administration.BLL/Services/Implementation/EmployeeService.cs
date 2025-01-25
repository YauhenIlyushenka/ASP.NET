using Pcf.Administration.BLL.Models;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain.Administration;
using Pcf.Administration.Core.Exceptions;

namespace Pcf.Administration.BLL.Services.Implementation
{
	public class EmployeeService : BaseService, IEmployeeService
	{
		private readonly IRepository<Employee, Guid> _employeeRepository;
		private readonly IRepository<GlobalRole, int> _roleRepository;

		public EmployeeService(
			IRepository<Employee, Guid> employeeRepository,
			IRepository<GlobalRole, int> roleRepository)
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
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

			return new EmployeeResponseDto
			{
				Id = employee.Id,
				Email = employee.Email,
				Role = new RoleItemResponseDto
				{
					Id = employee.RoleId,
					Name = employee.Role.Name,
					Description = employee.Role.Description,
				},
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto model)
		{
			var globalRole = await _roleRepository.GetByIdAsync((int)model.Role);

			var employee = await _employeeRepository.AddAsync(new Employee
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				RoleId = globalRole.Id,
				Role = new NestedRole
				{
					Name = globalRole.Name,
					Description = globalRole.Description,
				},
			});

			await _employeeRepository.SaveChangesAsync();

			return new EmployeeResponseDto
			{
				Id = employee.Id,
				Email = employee.Email,
				Role = new RoleItemResponseDto
				{
					Id = employee.RoleId,
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
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

			var globalRole = await _roleRepository.GetByIdAsync((int)model.Role);

			employee.FirstName = model.FirstName;
			employee.LastName = model.LastName;
			employee.Email = model.Email;
			employee.AppliedPromocodesCount = model.AppliedPromocodesCount;

			if (employee.RoleId != (int)model.Role)
			{
				employee.RoleId = globalRole.Id;
				employee.Role = new NestedRole
				{
					Name = globalRole.Name,
					Description = globalRole.Description,
				};
			}

			await _employeeRepository.UpdateAsync(employee);
			await _employeeRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Employee)));

			await _employeeRepository.DeleteAsync(employee);
			await _employeeRepository.SaveChangesAsync();
		}

		public async Task UpdateAppliedPromocodesAsync(Guid id)
		{
			var employee = await _employeeRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, $"{nameof(Employee)}/PartnerManager"));

			employee.AppliedPromocodesCount++;

			await _employeeRepository.UpdateAsync(employee);
			await _employeeRepository.SaveChangesAsync();
		}
	}
}
