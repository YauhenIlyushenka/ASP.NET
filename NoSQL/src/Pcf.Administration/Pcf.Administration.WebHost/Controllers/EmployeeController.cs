using Microsoft.AspNetCore.Mvc;
using Pcf.Administration.BLL.Models;
using Pcf.Administration.BLL.Services;
using Pcf.Administration.WebHost.Models.Request;
using Pcf.Administration.WebHost.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.Administration.WebHost.Controllers
{
	/// <summary>
	/// Employee
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class EmployeeController
	{
		private readonly IEmployeeService _employeeService;

		public EmployeeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		/// <summary>
		/// Get all employees
		/// </summary>S
		/// <returns></returns>
		[HttpGet]
		public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
		{
			var employees = await _employeeService.GetAllAsync();
			return employees.Select(x => new EmployeeShortResponse
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();
		}

		/// <summary>
		/// Get employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		public async Task<EmployeeResponse> GetEmployeeByIdAsync([FromRoute] Guid id)
		{
			var employee = await _employeeService.GetByIdAsync(id);

			return new EmployeeResponse
			{
				Id = employee.Id,
				Email = employee.Email,
				Role = new RoleItemResponse
				{
					Id = employee.Role.Id,
					Name = employee.Role.Name,
					Description = employee.Role.Description
				},
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		/// <summary>
		/// Create new employee
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<EmployeeResponse> CreateEmployeeAsync([FromBody] EmployeeRequest model)
		{
			var employee = await _employeeService.CreateAsync(new EmployeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = model.Role
			});

			return new EmployeeResponse
			{
				Id = employee.Id,
				Email = employee.Email,
				Role = new RoleItemResponse
				{
					Id = employee.Role.Id,
					Name = employee.Role.Name,
					Description = employee.Role.Description
				},
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};
		}

		/// <summary>
		/// Update employee
		/// </summary>
		/// <returns></returns>
		[HttpPut("{id:guid}")]
		public async Task UpdateEmployeeAsync([FromRoute] Guid id, [FromBody] EmployeeRequest model)
			=> await _employeeService.UpdateAsync(id, new EmployeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = model.Role
			});

		/// <summary>
		/// Delete employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id:guid}")]
		public async Task DeleteEmployeeAsync([FromRoute] Guid id)
			=> await _employeeService.DeleteAsync(id);

		/// <summary>
		/// Update count of provided promocodes
		/// </summary>
		/// <param name="id">Employee Id</param>
		/// <returns></returns>
		[HttpPost("{id:guid}/appliedPromocodes")]
		public async Task UpdateAppliedPromocodesAsync(Guid id)
			=> await _employeeService.UpdateAppliedPromocodesAsync(id);
	}
}
