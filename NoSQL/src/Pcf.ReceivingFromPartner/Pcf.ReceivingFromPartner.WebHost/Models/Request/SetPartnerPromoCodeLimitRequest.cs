namespace Pcf.ReceivingFromPartner.WebHost.Models.Request
{
	public class SetPartnerPromoCodeLimitRequest
	{
		public string EndDate { get; set; }
		public int Limit { get; set; }
	}
}