using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PromoCodeFactory.DataAccess.Abstarctions
{
	public abstract class BaseRepostory<T, TId> : IRepository<T, TId> where T : class, IEntity<TId>
	{
		private readonly DbSet<T> _entitySet;
		protected readonly DbContext Context;

		protected BaseRepostory(DbContext context)
		{
			Context = context;
			_entitySet = Context.Set<T>();
		}

		public virtual async Task<List<T>> GetAllAsync(bool asNoTracking = false)
			=> await GetAll().ToListAsync();

		public virtual async Task<T> GetByIdAsync(TId id)
			=> await _entitySet.FindAsync(id);

		public virtual async Task<T> AddAsync(T entity)
			=> (await _entitySet.AddAsync(entity)).Entity;

		public virtual void Update(T entity)
		{
			Context.Entry(entity).State = EntityState.Modified;
		}

		public bool Delete(T entity)
		{
			if (entity == null)
			{
				return false;
			}
			Context.Entry(entity).State = EntityState.Deleted;
			return true;
		}

		public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
			=> await Context.SaveChangesAsync(cancellationToken);

		private IQueryable<T> GetAll(bool asNoTracking = false)
		{
			return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
		}

	}
}
