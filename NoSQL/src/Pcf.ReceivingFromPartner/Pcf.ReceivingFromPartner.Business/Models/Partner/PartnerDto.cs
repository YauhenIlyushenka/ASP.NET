namespace Pcf.ReceivingFromPartner.Business.Models.Partner
{
	public class PartnerDto
	{
		public Guid Id { get; set; }
		public required string Name { get; init; }
		public int NumberIssuedPromoCodes { get; init; }
		public bool IsActive { get; init; }
		public List<PartnerPromoCodeLimitDto> PartnerLimits { get; set; } = [];
	}
}
