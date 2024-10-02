using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeResponseDto : BaseDto
	{
		public string FullName { get; init; }
		public string Email { get; init; }
		public int AppliedPromocodesCount { get; init; }
		public RoleItemResponseDto Role { get; init; }
	}
}
