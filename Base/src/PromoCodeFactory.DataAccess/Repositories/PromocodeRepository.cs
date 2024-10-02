using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class PromocodeRepository : BaseRepostory<PromoCode, Guid>, IRepository<PromoCode, Guid>
	{
		public PromocodeRepository(DatabaseContext context) : base(context)
		{ }
	}
}
