using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Response.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
	/// <summary>
	/// Roles of employees
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class RolesController : ControllerBase
	{
		private readonly IRoleService _roleService;

		public RolesController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		/// <summary>
		/// Get all roles of employees 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleItemResponse>))]
		public async Task<IActionResult> GetRolesAsync()
		{
			var roles = await _roleService.GetAllAsync();
			var rolesModel = roles.Select(x => new RoleItemResponse
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();

			return Ok(rolesModel);
		}
	}
}