using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.Administration
{
	public class Employee : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public string FullName => $"{FirstName} {LastName}";

		public string Email { get; set; }

		public int AppliedPromocodesCount { get; set; }

		public List<Role> Roles { get; set; }

		public void Update(
			string firstName,
			string lastName,
			string email,
			int appliedPromocodesCount,
			List<Role> roles)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			AppliedPromocodesCount = appliedPromocodesCount;
			Roles = roles;
		}
	}
}