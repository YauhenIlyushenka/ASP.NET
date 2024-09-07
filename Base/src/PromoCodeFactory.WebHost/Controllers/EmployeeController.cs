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
				Roles = employee.Roles.Select(x => new RoleItemResponse
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description
				}).ToList(),
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
		public async Task CreateEmployee([FromBody] EmployeeRequest model)
		{
			await _employeeService.CreateAsync(new EmpoyeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Role = model.Role
			});
		}

		/// <summary>
		/// Update employee
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		public async Task UpdateEmployee([FromBody] EmployeeRequestExtended model)
		{
			await _employeeService.UpdateAsync(new EmployeeRequestExtendedDto
			{
				Id = model.Id,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				Roles = model.Roles
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