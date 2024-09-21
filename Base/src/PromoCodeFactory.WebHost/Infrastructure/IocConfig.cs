using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.BusinessLogic.Services.Implementation;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.DataAccess.Repositories;
using PromoCodeFactory.WebHost.Infrastructure.Validators;
using PromoCodeFactory.WebHost.Models.Request.Employee;

namespace PromoCodeFactory.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddPromoCodeFactoryServices(this IServiceCollection services)
		{
			AddBussinessLogicServices(services);
			AddDataAccessLayerServices(services);
		}

		private static void AddBussinessLogicServices(this IServiceCollection services)
		{
			services.AddScoped<IEmployeeService, EmployeeService>();
			services.AddScoped<IRoleService, RoleService>();
		}

		private static void AddDataAccessLayerServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<Employee>), (x) => new InMemoryRepository<Employee>(FakeDataFactory.Employees));
			services.AddScoped(typeof(IRepository<Role>), (x) => new InMemoryRepository<Role>(FakeDataFactory.Roles));
			services.AddScoped(typeof(IRepository<Preference>), (x) => new InMemoryRepository<Preference>(FakeDataFactory.Preferences));
			services.AddScoped(typeof(IRepository<Customer>), (x) => new InMemoryRepository<Customer>(FakeDataFactory.Customers));
		}

		public static void AddValidators(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
		}
	}
}
