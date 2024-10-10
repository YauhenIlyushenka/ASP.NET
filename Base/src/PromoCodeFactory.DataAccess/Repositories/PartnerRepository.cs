using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class PartnerRepository : BaseRepostory<Partner, Guid>, IRepository<Partner, Guid>
	{
		public PartnerRepository(DatabaseContext context) : base(context)
		{ }
	}
}
