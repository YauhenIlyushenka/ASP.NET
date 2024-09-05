using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Services;
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
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<ActionResult<List<EmployeeShortResponse>>> GetEmployeesAsync()
		{
			var employees = await _employeeService.GetAllAsync();

			var employeesModelList = employees.Select(x => new EmployeeShortResponse
			{
				Id = x.Id,
				Email = x.Email,
				FullName = x.FullName,
			}).ToList();

			return Ok(employeesModelList);
		}

		/// <summary>
		/// Get Employee By Id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
		{
			var employee = await _employeeService.GetByIdAsync(id);

			if (employee == null)
			{
				return NotFound();
			}

			var employeeModel = new EmployeeResponse
			{
				Id = employee.Id,
				Email = employee.Email,
				Roles = employee.Roles.Select(x => new RoleItemResponse
				{
					Name = x.Name,
					Description = x.Description
				}).ToList(),
				FullName = employee.FullName,
				AppliedPromocodesCount = employee.AppliedPromocodesCount
			};

			return Ok(employeeModel);
		}
	}
}