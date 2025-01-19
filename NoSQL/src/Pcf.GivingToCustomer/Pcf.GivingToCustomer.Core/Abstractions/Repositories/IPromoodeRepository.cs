using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Core.Abstractions.Repositories
{
	public interface IPromoodeRepository : IMongoRepository<PromoCode, Guid>
	{
		Task<List<PromoCode>> GetPromocodesByIds(List<Guid> ids);
	}
}
