namespace Pcf.ReceivingFromPartner.Business.Models.Partner
{
	public class PartnerPromoCodeLimitDto
	{
		public Guid Id { get; set; }
		public int Limit { get; init; }
		public required string CreateDate { get; init; }
		public required string EndDate { get; init; }
		public string CancelDate { get; init; }

		public Guid PartnerId { get; init; }
		public PartnerDto Partner { get; init; }
	}
}
