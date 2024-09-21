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

		public Role Role { get; set; }

		public void Update(
			string firstName,
			string lastName,
			string email,
			int appliedPromocodesCount,
			Role role)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			AppliedPromocodesCount = appliedPromocodesCount;
			Role = role;
		}
	}
}