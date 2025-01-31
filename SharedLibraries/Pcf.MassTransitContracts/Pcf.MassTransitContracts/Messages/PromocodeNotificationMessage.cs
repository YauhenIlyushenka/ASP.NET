using System;

namespace Pcf.MassTransitContracts.Messages
{
	public class PromocodeNotificationMessage
	{
		public required string ServiceInfo { get; set; }
		public required string PromoCode { get; set; }
		public required string BeginDate { get; set; }
		public required string EndDate { get; set; }
		public int PreferenceId { get; set; }

		public Guid PartnerId { get; set; }
		public Guid? PartnerManagerId { get; set; }
	}
}
