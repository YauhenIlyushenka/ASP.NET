using PromoCodeFactory.Core.Domain.Enums;

namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public class CreateOrEditCustomerRequestDto
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public List<Preference> Preferences { get; init; }
	}
}
