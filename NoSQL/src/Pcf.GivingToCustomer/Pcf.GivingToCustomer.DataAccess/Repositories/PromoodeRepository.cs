using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Repositories
{
	public class PromoodeRepository : MongoRepository<PromoCode, Guid>, IPromoodeRepository
	{
		public PromoodeRepository(IMongoDatabase database) : base(database)
		{ }

		public async Task<List<PromoCode>> GetPromocodesByIds(List<Guid> ids)
			=> await Collection
				.Aggregate()
				.Match(c => ids.Contains(c.Id))
				.ToListAsync();
	}
}
