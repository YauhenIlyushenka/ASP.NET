using Pcf.GivingToCustomer.Core.Domain.Enums;

namespace Pcf.GivingToCustomer.BLL.Models
{
	public class CustomerResponseDto
	{
		public Guid Id { get; set; }
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required string Email { get; init; }
		public List<PreferenceResponseDto> Preferences { get; set; }
		public List<PromoCodeShortResponseDto> PromoCodes { get; set; }
	}
}
