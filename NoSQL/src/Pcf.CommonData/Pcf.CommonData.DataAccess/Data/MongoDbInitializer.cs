using MongoDB.Driver;
using Pcf.CommonData.Core.Domain;

namespace Pcf.CommonData.DataAccess.Data
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

			if (!preferencesCollection.AsQueryable().Any())
			{
				preferencesCollection.InsertMany(FakeDataFactory.Preferences);
			}
		}
	}
}
