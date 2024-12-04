namespace Pcf.Administration.BusinessLogic.Models
{
	public class EmployeeShortResponseDto
	{
		public Guid Id { get; set; }
		public required string FullName { get; init; }
		public required string Email { get; init; }
	}
}
