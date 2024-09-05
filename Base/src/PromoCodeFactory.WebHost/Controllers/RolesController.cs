﻿using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
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
	public class RolesController
	{
		private readonly IRepository<Role> _rolesRepository;

		public RolesController(IRepository<Role> rolesRepository)
		{
			_rolesRepository = rolesRepository;
		}

		/// <summary>
		/// Get all roles of employees 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<RoleItemResponse>> GetRolesAsync()
		{
			var roles = await _rolesRepository.GetAllAsync();

			var rolesModelList = roles.Select(x =>
				new RoleItemResponse()
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description
				}).ToList();

			return rolesModelList;
		}
	}
}