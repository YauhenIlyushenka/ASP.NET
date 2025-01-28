namespace Pcf.Administration.BLL.Models
{
	public class EmployeeResponseDto
	{
		public Guid Id { get; set; }
		public required string FullName { get; init; }
		public required string Email { get; init; }
		public int AppliedPromocodesCount { get; init; }
		public RoleItemResponseDto Role { get; init; }
	}
}
