using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeResponseDto : BaseDto
	{
		public required string FullName { get; init; }
		public required string Email { get; init; }
		public int AppliedPromocodesCount { get; init; }
		public RoleItemResponseDto Role { get; init; }
	}
}
