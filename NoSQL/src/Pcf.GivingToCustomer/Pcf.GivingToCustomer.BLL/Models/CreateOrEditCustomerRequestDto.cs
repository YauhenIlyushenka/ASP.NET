using Pcf.GivingToCustomer.Core.Domain.Enums;

namespace Pcf.GivingToCustomer.BLL.Models
{
	public class CreateOrEditCustomerRequestDto
	{
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
		public List<Preference> Preferences { get; init; }
	}
}
