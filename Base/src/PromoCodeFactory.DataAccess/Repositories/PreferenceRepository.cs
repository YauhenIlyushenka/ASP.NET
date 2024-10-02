using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class PreferenceRepository : BaseRepostory<Preference, Guid>, IRepository<Preference, Guid>
	{
		public PreferenceRepository(DatabaseContext context) : base(context)
		{ }
	}
}
