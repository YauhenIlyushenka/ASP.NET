namespace Pcf.GivingToCustomer.BLL.Models
{
	public class PromoCodeShortResponseDto
	{
		public Guid Id { get; set; }
		public required string Code { get; init; }
		public required string ServiceInfo { get; set; }
		public required string BeginDate { get; set; }
		public required string EndDate { get; set; }
		public Guid PartnerId { get; set; }
	}
}
