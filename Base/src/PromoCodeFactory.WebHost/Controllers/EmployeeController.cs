using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Models.Employee;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Request.Employee;
using PromoCodeFactory.WebHost.Models.Response.Employee;
using PromoCodeFactory.WebHost.Models.Response.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
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
			var employeesModels = employees.Select(x => new EmployeeShortResponse
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

			return employeesModels;
		}

		/// <summary>
		/// Get employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		public async Task<EmployeeResponse> GetEmployeeByIdAsync(Guid id)
		{
			var employee = await _employeeService.GetByIdAsync(id);
			var employeeModel = new EmployeeResponse
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

			return employeeModel;
		}

		/// <summary>
		/// Create new employee
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<EmployeeResponse> CreateEmployee([FromBody] EmployeeRequest model)
		{
			var employee = await _employeeService.CreateAsync(new EmployeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = model.Role
			});

			var employeeModel = new EmployeeResponse
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

			return employeeModel;
		}

		/// <summary>
		/// Update employee
		/// </summary>
		/// <returns></returns>
		[HttpPut("{id:guid}")]
		public async Task UpdateEmployee([FromRoute] Guid id, [FromBody] EmployeeRequest model)
		{
			await _employeeService.UpdateAsync(id, new EmployeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = model.Role
			});
		}

		/// <summary>
		/// Delete employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id:guid}")]
		public async Task DeleteEmployee(Guid id)
		{
			await _employeeService.DeleteAsync(id);
		}
	}
}