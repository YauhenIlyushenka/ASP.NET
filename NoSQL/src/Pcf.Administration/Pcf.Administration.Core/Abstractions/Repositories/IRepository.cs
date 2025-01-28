using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Pcf.Administration.Core.Domain;

namespace Pcf.Administration.Core.Abstractions.Repositories
{
	public interface IRepository<T, TId> where T: IBaseEntity<TId>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(TId id);
		Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}