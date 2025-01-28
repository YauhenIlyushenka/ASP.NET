using Pcf.ReceivingFromPartner.Core.Domain.Enum;
using System;

namespace Pcf.ReceivingFromPartner.WebHost.Models.Request
{
	public class ReceivingPromoCodeRequest
	{
		public string ServiceInfo { get; set; }
		public string PromoCode { get; set; }
		public Preference Preference { get; set; }
		public Guid? PartnerManagerId { get; set; }
	}
}
