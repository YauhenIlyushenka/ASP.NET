using Microsoft.AspNetCore.Http;
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
	/// Employees
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class EmployeesController : ControllerBase
	{
		private readonly IEmployeeService _employeeService;

		public EmployeesController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		/// <summary>
		/// Get all employees
		/// </summary>S
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeShortResponse>))]
		public async Task<IActionResult> GetEmployeesAsync()
		{
			var employees = await _employeeService.GetAllAsync();
			var employeesModels = employees.Select(x => new EmployeeShortResponse
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

			return Ok(employeesModels);
		}

		/// <summary>
		/// Get employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeShortResponse))]
		public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
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

			return Ok(employeeModel);
		}

		/// <summary>
		/// Create new employee
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest model)
		{
			await _employeeService.CreateAsync(new EmpoyeeRequestDto
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				RoleId = model.RoleId
			});

			return Ok();
		}

		/// <summary>
		/// Update employee
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRequestExtended model)
		{
			await _employeeService.UpdateAsync(new EmployeeRequestExtendedDto
			{
				Id = model.Id,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				AppliedPromocodesCount = model.AppliedPromocodesCount,
				RoleIds = model.RoleIds
			});

			return Ok();
		}

		/// <summary>
		/// Delete employee by Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteEmployee(Guid id)
		{
			await _employeeService.DeleteAsync(id);

			return Ok();
		}
	}
}