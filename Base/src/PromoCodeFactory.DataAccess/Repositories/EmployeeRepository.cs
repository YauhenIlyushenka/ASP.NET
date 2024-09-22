using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class EmployeeRepository : BaseRepostory<Employee, Guid>, IRepository<Employee, Guid>
	{
		public EmployeeRepository(DatabaseContext context) : base(context)
		{ }
	}
}
