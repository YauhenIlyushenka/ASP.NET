using System;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public Guid RoleId { get; set; }
		public int AppliedPromocodesCount { get; set; }
	}
}
