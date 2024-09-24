namespace PromoCodeFactory.BusinessLogic.Models.PromoCode
{
	public class GivePromoCodeRequestDto
	{
		public string ServiceInfo { get; init; }
		public string PartnerName { get; init; }
		public string PromoCode { get; init; }
		public string Preference { get; init; }
	}
}
