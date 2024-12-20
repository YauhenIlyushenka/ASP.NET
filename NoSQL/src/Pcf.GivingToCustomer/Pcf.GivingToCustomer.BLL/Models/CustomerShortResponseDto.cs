namespace Pcf.GivingToCustomer.BLL.Models
{
	public class CustomerShortResponseDto
	{
		public Guid Id { get; set; }
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
	}
}
