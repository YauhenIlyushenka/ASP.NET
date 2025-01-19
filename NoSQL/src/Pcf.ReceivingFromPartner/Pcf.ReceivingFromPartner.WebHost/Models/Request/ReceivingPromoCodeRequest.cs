using Pcf.ReceivingFromPartner.Core.Domain.Enum;
using System;

namespace Pcf.ReceivingFromPartner.WebHost.Models.Request
{
	/// <example>
	///{
	///    "serviceInfo": "Билеты на лучший спектакль сезона",
	///    "promoCode": "H123124",
	///    "preferenceId": "ef7f299f-92d7-459f-896e-078ed53ef99c",
	///    "partnerManagerId": "451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"
	///}
	/// </example>
	public class ReceivingPromoCodeRequest
	{
		public string ServiceInfo { get; set; }
		public string PromoCode { get; set; }
		public Preference Preference { get; set; }
		public Guid? PartnerManagerId { get; set; }
	}
}
