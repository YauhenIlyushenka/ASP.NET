using PromoCodeFactory.Core.Domain.Enums;

namespace PromoCodeFactory.WebHost.Models.Request.PromoCode
{
	public class GivePromoCodeRequest
	{
		public string ServiceInfo { get; set; }
		public string PartnerName { get; set; }
		public string PromoCode { get; set; }
		public Preference Preference { get; set; }
	}
}
