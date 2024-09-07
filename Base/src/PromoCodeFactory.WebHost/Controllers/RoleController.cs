using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Response.Role;
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
	public class RoleController
	{
		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		/// <summary>
		/// Get all roles of employees 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<RoleItemResponse>> GetRolesAsync()
		{
			var roles = await _roleService.GetAllAsync();
			var rolesModel = roles.Select(x => new RoleItemResponse
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();

			return rolesModel;
		}
	}
}