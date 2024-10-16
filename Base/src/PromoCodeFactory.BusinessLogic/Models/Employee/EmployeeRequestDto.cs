using EnumRole = PromoCodeFactory.Core.Domain.Enums.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeRequestDto
	{
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
		public EnumRole Role { get; init; }
		public int AppliedPromocodesCount { get; init; }
	}
}
