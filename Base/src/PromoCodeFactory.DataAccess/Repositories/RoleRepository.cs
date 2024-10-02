using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class RoleRepository : BaseRepostory<Role, Guid>, IRepository<Role, Guid>
	{
		public RoleRepository(DatabaseContext context) : base(context)
		{ }
	}
}
