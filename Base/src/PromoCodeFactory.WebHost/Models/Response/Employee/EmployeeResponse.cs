using PromoCodeFactory.WebHost.Models.Response.Role;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models.Response.Employee
{
	public class EmployeeResponse
	{
		public Guid Id { get; set; }

		public string FullName { get; set; }

		public string Email { get; set; }

		public List<RoleItemResponse> Roles { get; set; }

		public int AppliedPromocodesCount { get; set; }
	}
}
