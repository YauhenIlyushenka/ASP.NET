using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.Administration
{
	public class Role : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string DescriptionRole { get; set; }
		public ICollection<Employee> Employees { get; set; }

		public Role()
		{
			Employees = new List<Employee>();
		}
	}
}