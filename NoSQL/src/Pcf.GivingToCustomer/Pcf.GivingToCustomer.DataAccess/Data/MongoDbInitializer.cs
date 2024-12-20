using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Domain;
using System.Linq;

namespace Pcf.GivingToCustomer.DataAccess.Data
{
	public class MongoDbInitializer : IMongoDbInitializer
	{
		private readonly IMongoDatabase _mongoDatabase;

		public MongoDbInitializer(IMongoDatabase database)
		{
			_mongoDatabase = database;
		}
		
		public void Seed()
		{
			var preferencesCollection = _mongoDatabase.GetCollection<Preference>("preferences");
			var customersCollection = _mongoDatabase.GetCollection<Customer>("customers");

			if (!preferencesCollection.AsQueryable().Any())
			{
				preferencesCollection.InsertMany(FakeDataFactory.Preferences);
			}

			if (!customersCollection.AsQueryable().Any())
			{
				customersCollection.InsertMany(FakeDataFactory.Customers);
			}
		}
	}
}