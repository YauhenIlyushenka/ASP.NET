using EnumPreference = PromoCodeFactory.Core.Domain.Enums.Preference;

namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public class CreateOrEditCustomerRequestDto
	{
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
		public List<EnumPreference> Preferences { get; init; }
	}
}
