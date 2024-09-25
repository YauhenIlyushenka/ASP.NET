using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.Administration
{
	public class Employee : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string Email { get; set; }
		public int AppliedPromocodesCount { get; set; }

		public Guid RoleId { get; set; }
		public Role Role { get; set; }

		public ICollection<PromoCode> PromoCodes { get; set; }

		public Employee()
		{
			PromoCodes = new List<PromoCode>();
		}
	}
}