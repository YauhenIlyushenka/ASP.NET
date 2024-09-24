namespace PromoCodeFactory.BusinessLogic.Models.PromoCode
{
	public class PromoCodeShortResponseDto: BaseDto
	{
		public string Code { get; init; }

		public string ServiceInfo { get; init; }

		public string BeginDate { get; init; }

		public string EndDate { get; init; }

		public string PartnerName { get; init; }
	}
}
