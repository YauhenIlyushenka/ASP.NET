using MongoDB.Bson;
using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Repositories
{
	public class MongoRepository<T, TId> : IMongoRepository<T, TId> where T : class
	{
		private const string PrimaryKey = "_id";
		private readonly IMongoCollection<T> _collection;

		private static readonly Dictionary<Type, Func<TId, object>> IdConverter = new()
		{
			[typeof(string)] = id => new ObjectId(id as string),
			[typeof(Guid)] = id => id is Guid guid ? guid.ToString() : throw new ArgumentException("Invalid Guid type"),
			[typeof(int)] = id => id,
		};

		public IMongoCollection<T> Collection => _collection;

		public MongoRepository(IMongoDatabase database)
		{
			var collectionName = typeof(T).Name.ToLower();
			_collection = database.GetCollection<T>($"{collectionName}s");
		}

		public async Task<List<T>> GetAllAsync()
			=> await _collection.Find(new BsonDocument()).ToListAsync();

		public async Task<List<T>> GetAllAsync(FilterDefinition<T> filter = null)
		{
			filter ??= Builders<T>.Filter.Empty;
			return await _collection.Find(filter).ToListAsync();
		}

		public async Task<T> GetByIdAsync(TId id)
			=> await _collection.Find(Builders<T>.Filter.Eq(PrimaryKey, ConvertId(id))).FirstOrDefaultAsync();

		public async Task AddAsync(T entity)
			=> await _collection.InsertOneAsync(entity);

		public async Task UpdateAsync(TId id, T entity)
			=> await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq(PrimaryKey, ConvertId(id)), entity);

		public async Task DeleteAsync(TId id)
			=> await _collection.DeleteOneAsync(Builders<T>.Filter.Eq(PrimaryKey, ConvertId(id)));

		public async Task DeleteManyByIdsAsync(IEnumerable<TId> ids)
		{
			var filter = Builders<T>.Filter.In(PrimaryKey, ids.Select(ConvertId));
			await _collection.DeleteManyAsync(filter);
		}

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
