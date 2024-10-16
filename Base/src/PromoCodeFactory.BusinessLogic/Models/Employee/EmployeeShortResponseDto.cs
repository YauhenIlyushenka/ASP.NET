namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeShortResponseDto : BaseDto
	{
		public required string FullName { get; init; }
		public required string Email { get; init; }
	}
}