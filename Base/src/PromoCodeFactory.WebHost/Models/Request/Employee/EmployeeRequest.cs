using PromoCodeFactory.Core.Domain.Administration.Enum;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequest : BaseEmployeeRequest
	{
		public Role Role { get; set; }
	}
}
