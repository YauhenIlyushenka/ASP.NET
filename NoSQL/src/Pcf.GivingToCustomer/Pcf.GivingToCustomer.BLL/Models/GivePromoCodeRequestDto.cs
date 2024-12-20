using Pcf.GivingToCustomer.Core.Domain.Enums;

namespace Pcf.GivingToCustomer.BLL.Models
{
	public class GivePromoCodeRequestDto
	{
		public required string ServiceInfo { get; init; }
		public required string PromoCode { get; init; }
		public required string BeginDate { get; init; }
		public required string EndDate { get; init; }
		public Guid PartnerId { get; init; }
		public Preference Preference { get; init; }
	}
}
