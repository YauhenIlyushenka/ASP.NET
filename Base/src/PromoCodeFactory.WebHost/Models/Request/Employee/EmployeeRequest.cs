using PromoCodeFactory.Core.Domain.Enums;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequest : BaseCommonRequest
	{
		public int AppliedPromocodesCount { get; set; }
		public Role Role { get; set; }
	}
}
