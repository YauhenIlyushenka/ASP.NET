using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, bool asNoTracking = false)
		{
			IQueryable<T> query = _entitySet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (asNoTracking)
			{
				query = query.AsNoTracking();
			}

			return await query.ToListAsync();
		}

		public virtual async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, string includes = null)
		{
			IQueryable<T> query = _entitySet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includes == null || includes.Length == 0)
			{
				foreach (var includeEntity in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeEntity);
				}
			}
			
			return await query.SingleOrDefaultAsync();
		}

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
	}
}
