namespace PromoCodeFactory.WebHost.Models.Request.Partner
{
	public class SetPartnerPromoCodeLimitRequest
	{
		public string EndDate { get; set; }
		public int Limit { get; set; }
	}
}
