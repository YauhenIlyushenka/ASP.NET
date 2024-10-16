namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public abstract class CustomerResponseBaseModel : BaseDto
	{
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
	}
}
