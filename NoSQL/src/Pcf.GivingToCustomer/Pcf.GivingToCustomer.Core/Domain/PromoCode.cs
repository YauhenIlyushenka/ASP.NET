using System;

namespace Pcf.GivingToCustomer.Core.Domain
{
	public class PromoCode : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string ServiceInfo { get; set; }
		public DateTime BeginDate { get; set; }
		public DateTime EndDate { get; set; }
		public Guid PartnerId { get; set; }
		public int PreferenceId { get; set; }
		public Guid CustomerId { get; set; }
	}
}