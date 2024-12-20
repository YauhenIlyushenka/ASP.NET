using System;

namespace Pcf.Administration.Core.Domain.Administration
{
	public class Employee : IBaseEntity<Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public string Email { get; set; }

		public int RoleId { get; set; }
		public NestedRole Role { get; set; }

		public int AppliedPromocodesCount { get; set; }
	}
}