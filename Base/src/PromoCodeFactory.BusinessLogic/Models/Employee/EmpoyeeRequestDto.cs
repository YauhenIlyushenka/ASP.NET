using EnumRole = PromoCodeFactory.Core.Domain.Administration.Enum.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmpoyeeRequestDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public EnumRole Role { get; set; }
		public int AppliedPromocodesCount { get; set; }
	}
}
