using Pcf.ReceivingFromPartner.Core.Domain.Enum;

namespace Pcf.ReceivingFromPartner.Business.Models
{
	public class ReceivingPromoCodeRequestDto
	{
		public required string ServiceInfo { get; set; }
		public required string PromoCode { get; set; }
		public Preference Preference { get; set; }
		public Guid? PartnerManagerId { get; set; }
	}
}
