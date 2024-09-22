using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
	public class Customer : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string Email { get; set; }

		public ICollection<PromoCode> PromoCodes { get; set; }
		public ICollection<Preference> Preferences { get; set; }

		public Customer()
		{
			PromoCodes = new List<PromoCode>();
			Preferences = new List<Preference>();
		}
	}
}
