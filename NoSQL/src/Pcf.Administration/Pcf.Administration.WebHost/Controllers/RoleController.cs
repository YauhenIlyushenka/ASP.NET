using Microsoft.AspNetCore.Mvc;
using Pcf.Administration.BLL.Services;
using Pcf.Administration.WebHost.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.Administration.WebHost.Controllers
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

			return roles.Select(x => new RoleItemResponse
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();
		}
	}
}
