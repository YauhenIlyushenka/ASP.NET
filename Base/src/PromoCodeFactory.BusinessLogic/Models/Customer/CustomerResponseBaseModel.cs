namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public abstract class CustomerResponseBaseModel : BaseDto
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
	}
}
