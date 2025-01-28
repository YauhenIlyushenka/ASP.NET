using MongoDB.Bson;
using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Repositories
{
	public class CustomerRepository : MongoRepository<Customer, Guid>, ICustomerRepository
	{
		public CustomerRepository(IMongoDatabase database) : base(database)
		{ }

		public async Task<List<Customer>> GetCustomersWithPreference(int prefereneId)
		{
			var customers = await Collection
				.Aggregate()
				.Match(c => c.PreferenceIds.Contains(prefereneId))
				.ToListAsync();

			return customers;
		}

		public async Task<BsonDocument> GetCustomerWithPreferencesAndPromocodes(Guid id)
		{
			var customerBsonDocument = await Collection
				.Aggregate()
				.Match(c => c.Id == id)
				.Lookup(
					foreignCollectionName: "preferences",
					localField: "PreferenceIds",
					foreignField: "_id",
					@as: "Preferences")
				.Lookup(
					foreignCollectionName: "promocodes",
					localField: "PromoCodeIds",
					foreignField: "_id",
					@as: "PromoCodes")
				.FirstOrDefaultAsync();

			return customerBsonDocument;
		}

		public async Task UpdateCustomersWithNewPromocode(List<CustomerPromocodeResult> customerPromocodeResults)
		{
			var bulkUpdateForCustomers = new List<WriteModel<Customer>>();

			foreach (var result in customerPromocodeResults)
			{
				var updateCustomer = Builders<Customer>.Update.Push(c => c.PromoCodeIds, result.PromocodeId);
				bulkUpdateForCustomers.Add(new UpdateOneModel<Customer>(
					Builders<Customer>.Filter.Eq(c => c.Id, result.CustomerId),
					updateCustomer));
			}

			if (bulkUpdateForCustomers.Any())
			{
				await Collection.BulkWriteAsync(bulkUpdateForCustomers);
			}
		}
	}
}