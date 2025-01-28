using Pcf.GivingToCustomer.Core.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Pcf.GivingToCustomer.WebHost.Models
{
	public class GivePromoCodeRequest
	{
		[JsonPropertyName("serviceinfo")]
		public string ServiceInfo { get; set; }

		[JsonPropertyName("promocode")]
		public string PromoCode { get; set; }

		[JsonPropertyName("begindate")]
		public string BeginDate { get; set; }

		[JsonPropertyName("enddate")]
		public string EndDate { get; set; }

		[JsonPropertyName("preference")]
		public Preference Preference { get; set; }

		[JsonPropertyName("partnerid")]
		public Guid PartnerId { get; set; }

		[JsonPropertyName("partnermanagerId")]
		public Guid? PartnerManagerId { get; set; }
	}
}