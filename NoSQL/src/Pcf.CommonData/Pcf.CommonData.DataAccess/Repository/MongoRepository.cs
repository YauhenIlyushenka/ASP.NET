using MongoDB.Bson;
using MongoDB.Driver;
using Pcf.CommonData.Core.Core.Abstractions;

namespace Pcf.CommonData.DataAccess.Repository
{
	public class MongoRepository<T, TId> : IMongoRepository<T, TId> where T : class
	{
		private const string PrimaryKey = "_id";

		private readonly IMongoCollection<T> _collection;

		private static readonly Dictionary<Type, Func<TId, object>> IdConverter = new()
		{
			[typeof(int)] = id => id,
		};

		public MongoRepository(IMongoDatabase database)
		{
			var collectionName = typeof(T).Name.ToLower();
			_collection = database.GetCollection<T>($"{collectionName}s");
		}

		public async Task<List<T>> GetAllAsync()
			=> await _collection.Find(new BsonDocument()).ToListAsync();

		public async Task<T> GetByIdAsync(TId id)
			=> await _collection.Find(Builders<T>.Filter.Eq(PrimaryKey, ConvertId(id))).FirstOrDefaultAsync();

		private object ConvertId(TId id)
		{
			var idType = typeof(TId);
			if (!IdConverter.ContainsKey(idType))
			{
				throw new ArgumentException($"Unsupported ID type: {idType.Name}");
			}

			return IdConverter[idType](id);
		}
	}
}
