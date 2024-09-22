using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.BusinessLogic.Services.Implementation;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.DataAccess.Repositories;
using System;

namespace PromoCodeFactory.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddPromoCodeFactoryServices(this IServiceCollection services)
		{
			AddBussinessLogicServices(services);
			AddDataAccessLayerRepositories(services);
		}

		private static void AddBussinessLogicServices(this IServiceCollection services)
		{
			services.AddScoped<IEmployeeService, EmployeeService>();
			services.AddScoped<IRoleService, RoleService>();
		}

		private static void AddDataAccessLayerRepositories(this IServiceCollection services)
		{
			services.AddScoped<IRepository<Employee, Guid>, EmployeeRepository>();
			services.AddScoped<IRepository<Role, Guid>, RoleRepository>();
		}
	}
}
