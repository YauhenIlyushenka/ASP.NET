using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeResponseDto : BaseDto
	{
		public string FullName { get; set; }

		public string Email { get; set; }

		public int AppliedPromocodesCount { get; set; }

		public List<RoleItemResponseDto> Roles { get; set; } 
	}
}
