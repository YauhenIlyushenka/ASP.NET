using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
	public class Partner : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int NumberIssuedPromoCodes { get; set; }
		public bool IsActive { get; set; }
		public ICollection<PartnerPromoCodeLimit> PartnerLimits { get; set; }
	}
}
