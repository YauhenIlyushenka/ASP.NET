using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Core.Abstractions.Repositories
{
	public interface IMongoRepository<T, TId> where T : class
	{
		Task<List<T>> GetAllAsync();
		Task<List<T>> GetAllAsync(FilterDefinition<T> filter = null);
		Task<T> GetByIdAsync(TId id);
		Task AddAsync(T entity);
		Task UpdateAsync(TId id, T entity);
		Task DeleteAsync(TId id);
		Task DeleteManyByIdsAsync(IEnumerable<TId> ids);
		IMongoCollection<T> Collection { get; }
	}
}
