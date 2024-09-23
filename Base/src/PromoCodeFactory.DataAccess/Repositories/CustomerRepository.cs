using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Abstarctions;
using System;

namespace PromoCodeFactory.DataAccess.Repositories
{
	public class CustomerRepository : BaseRepostory<Customer, Guid>, IRepository<Customer, Guid>
	{
		public CustomerRepository(DatabaseContext context) : base(context)
		{ }
	}
}
