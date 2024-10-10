namespace PromoCodeFactory.BusinessLogic.Models.Partner
{
	public class PartnerDto : BaseDto
	{
		public string Name { get; init; }
		public int NumberIssuedPromoCodes { get; init; }
		public bool IsActive { get; init; }
		public List<PartnerPromoCodeLimitDto> PartnerLimits { get; set; } = [];
	}
}
