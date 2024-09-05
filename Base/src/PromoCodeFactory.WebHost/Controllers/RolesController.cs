using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Models.Role;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Request.Role;
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

		/// <summary>
		/// Get role by Id
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleItemResponse))]
		public async Task<IActionResult> GetRoleByIdAsync(Guid id)
		{
			var role = await _roleService.GetByIdAsync(id);

			if (role == null)
			{
				return NotFound();
			}

			var roleModel = new RoleItemResponse
			{
				Id = role.Id,
				Name = role.Name,
				Description = role.Description
			};

			return Ok(roleModel);
		}

		/// <summary>
		/// Create new role
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult> CreateRole([FromBody] RoleItemRequest model)
		{
			await _roleService.CreateAsync(new RoleItemRequestDto
			{
				Name = model.Name,
				Description = model.Description
			});

			return Ok();
		}

		/// <summary>
		/// Update Role
		/// </summary>
		/// <returns></returns>
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateRole([FromBody] RoleItemRequestExtended model)
		{
			await _roleService.UpdateAsync(new RoleItemRequestDto
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description
			});

			return Ok();
		}

		/// <summary>
		/// Delete role by Id
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id:guid}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteRole(Guid id)
		{
			await _roleService.DeleteAsync(id);

			return Ok();
		}
	}
}