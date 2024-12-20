using System;
using System.Collections.Generic;

namespace Pcf.GivingToCustomer.Core.Domain
{
	public class Preference : IEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Guid> PromoCodeIds { get; set; }
		public ICollection<Guid> CustomerIds { get; set; }
	}
}