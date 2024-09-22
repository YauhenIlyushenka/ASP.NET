using PromoCodeFactory.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Repositories
{
	public interface IRepository<T, TId> where T : IEntity<TId>
	{
		Task<List<T>> GetAllAsync(bool asNoTracking = false);

		Task<T> GetByIdAsync(TId id);

		Task<T> AddAsync(T entity);

		void Update(T entity);

		bool Delete(T entity);

		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
