using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequestExtended
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int AppliedPromocodesCount { get; set; }

		public List<Guid> RoleIds { get; set; }
	}
}
