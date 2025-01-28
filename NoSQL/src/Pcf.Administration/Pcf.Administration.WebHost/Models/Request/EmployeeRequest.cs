using Pcf.Administration.Core.Domain.Enums;

namespace Pcf.Administration.WebHost.Models.Request
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
