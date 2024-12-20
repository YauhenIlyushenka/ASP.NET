using System;
using System.Collections.Generic;

namespace Pcf.GivingToCustomer.Core.Domain
{
	public class Customer : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string Email { get; set; }
		public ICollection<int> PreferenceIds { get; set; }
		public ICollection<Guid> PromoCodeIds { get; set; }
	}
}