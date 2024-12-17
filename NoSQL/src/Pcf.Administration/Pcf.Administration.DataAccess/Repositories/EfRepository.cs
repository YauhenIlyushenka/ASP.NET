using Microsoft.EntityFrameworkCore;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pcf.Administration.DataAccess.Repositories
{
	public class EfRepository<T, TId> : IRepository<T, TId> where T : class, IBaseEntity<TId>
	{
		private readonly DbSet<T> _entitySet;
		protected readonly DataContext Context;

		public EfRepository(DataContext dataContext)
		{
			Context = dataContext;
			_entitySet = Context.Set<T>();
		}
		
		public virtual async Task<IEnumerable<T>> GetAllAsync()
			=> await _entitySet.ToListAsync();

		public virtual async Task<T> GetByIdAsync(TId id)
		{
			var predicate = BuildIdPredicate(id);
			return await _entitySet.FirstOrDefaultAsync(predicate);
		}

		public virtual async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
			=> await _entitySet.FirstOrDefaultAsync(predicate);

		public virtual async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
			=> await _entitySet.Where(predicate).ToListAsync();

		public virtual async Task<T> AddAsync(T entity)
			=> (await _entitySet.AddAsync(entity)).Entity;

		public virtual Task UpdateAsync(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
			return Task.CompletedTask;
		}

		public virtual Task DeleteAsync(T entity)
		{
			_entitySet.Remove(entity);
			Context.Entry(entity).State = EntityState.Deleted;
			return Task.CompletedTask;
		}

		public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
			=> await Context.SaveChangesAsync(cancellationToken);

		private Expression<Func<T, bool>> BuildIdPredicate(TId id)
		{
			var parameter = Expression.Parameter(typeof(T), "x");
			var property = Expression.Property(parameter, "Id");

			var constant = Expression.Constant(id);
			var body = Expression.Equal(property, constant);

			return Expression.Lambda<Func<T, bool>>(body, parameter);
		}
	}
}