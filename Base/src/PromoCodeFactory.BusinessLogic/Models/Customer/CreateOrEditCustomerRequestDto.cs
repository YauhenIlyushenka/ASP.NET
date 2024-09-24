using EnumPreference = PromoCodeFactory.Core.Domain.Enums.Preference;

namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public class CreateOrEditCustomerRequestDto
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public List<EnumPreference> Preferences { get; init; }
	}
}
