using MongoDB.Bson;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Core.Abstractions.Repositories
{
	public interface ICustomerRepository : IMongoRepository<Customer, Guid>
	{
		Task<BsonDocument> GetCustomerWithPreferencesAndPromocodes(Guid id);
		Task<List<Customer>> GetCustomersWithPreference(int prefereneId);
		Task UpdateCustomersWithNewPromocode(List<CustomerPromocodeResult> customerPromocodeResults);
	}
}