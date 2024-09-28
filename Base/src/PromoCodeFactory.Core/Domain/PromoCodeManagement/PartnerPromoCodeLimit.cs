using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
	public class PartnerPromoCodeLimit
	{
		public Guid Id { get; set; }
		public int Limit { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime? CancelDate { get; set; }

		public Guid PartnerId { get; set; }
		public Partner Partner { get; set; }
	}
}
