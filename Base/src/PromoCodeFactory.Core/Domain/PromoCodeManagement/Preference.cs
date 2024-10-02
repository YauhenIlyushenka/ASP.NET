using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
	public class Preference : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public ICollection<PromoCode> Promocodes { get; set; }
		public ICollection<Customer> Customers { get; set; }

		public Preference()
		{
			Customers = new List<Customer>();
			Promocodes = new List<PromoCode>();
		}
	}
}
