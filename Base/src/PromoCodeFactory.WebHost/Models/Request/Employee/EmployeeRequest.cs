using PromoCodeFactory.Core.Domain.Administration.Enum;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int AppliedPromocodesCount { get; set; }
		public Role Role { get; set; }
	}
}
