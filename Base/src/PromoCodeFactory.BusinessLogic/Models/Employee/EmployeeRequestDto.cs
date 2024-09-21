using EnumRole = PromoCodeFactory.Core.Domain.Administration.Enum.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeRequestDto
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public EnumRole Role { get; init; }
		public int AppliedPromocodesCount { get; init; }
	}
}
