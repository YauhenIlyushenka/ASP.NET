using Pcf.ReceivingFromPartner.Core.Domain.Enum;
using System;

namespace Pcf.ReceivingFromPartner.Integration.Dto
{
	public class GivePromoCodeToCustomerDto
	{
		public string ServiceInfo { get; set; }
		public string PromoCode { get; set; }
		public string BeginDate { get; set; }
		public string EndDate { get; set; }
		public Preference Preference { get; set; }

		public Guid PartnerId { get; set; }
		public Guid PromoCodeId { get; set; }
		public Guid? PartnerManagerId { get; set; }
	}
}