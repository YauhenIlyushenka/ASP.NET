﻿using System;

namespace Pcf.ReceivingFromPartner.Core.Domain
{
	public class PartnerPromoCodeLimit : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public int Limit { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? CancelDate { get; set; }
		public DateTime EndDate { get; set; }

		public Guid PartnerId { get; set; }
		public virtual Partner Partner { get; set; }
	}
}