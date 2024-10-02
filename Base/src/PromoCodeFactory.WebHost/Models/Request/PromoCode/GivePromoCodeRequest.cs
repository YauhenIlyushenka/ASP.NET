using PromoCodeFactory.Core.Domain.Enums;
using System;

namespace PromoCodeFactory.WebHost.Models.Request.PromoCode
{
	public class GivePromoCodeRequest
	{
		public string ServiceInfo { get; set; }
		public string PromoCode { get; set; }
		public string BeginDate { get; set; }
		public string EndDate { get; set; }
		public string PartnerName { get; set; }
		public Guid EmployeeId { get; set; }
		public Preference Preference { get; set; }
	}
}
