namespace PromoCodeFactory.BusinessLogic.Models.Partner
{
	public class PartnerPromoCodeLimitDto : BaseDto
	{
		public int Limit { get; init; }
		public string CreateDate { get; init; }
		public string EndDate { get; init; }
		public string CancelDate { get; init; }

		public Guid PartnerId { get; init; }
		public PartnerDto Partner { get; init; }
	}
}
