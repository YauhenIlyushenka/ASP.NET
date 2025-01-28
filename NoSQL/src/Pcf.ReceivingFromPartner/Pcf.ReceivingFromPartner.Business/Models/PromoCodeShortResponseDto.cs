namespace Pcf.ReceivingFromPartner.Business.Models
{
	public class PromoCodeShortResponseDto
	{
		public Guid Id { get; set; }
		public  required string Code { get; init; }
		public required string ServiceInfo { get; init; }
		public required string BeginDate { get; init; }
		public required string EndDate { get; init; }

		public Guid PartnerId { get; init; }
		public required string PartnerName { get; init; }
	}
}
