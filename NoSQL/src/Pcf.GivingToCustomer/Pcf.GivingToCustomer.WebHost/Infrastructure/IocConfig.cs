using Microsoft.Extensions.DependencyInjection;
using Pcf.GivingToCustomer.BLL.Services;
using Pcf.GivingToCustomer.BLL.Services.Implementation;
using Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.DataAccess.Data;
using Pcf.GivingToCustomer.DataAccess.Repositories;
using Pcf.GivingToCustomer.Integration;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddGivingToCustomerServices(this IServiceCollection services)
		{
			AddGivingToCustomerLogicServices(services);
			AddGivingToCustomerNotificationServices(services);
			AddGivingToCustomerRepositories(services);
		}

		private static void AddGivingToCustomerLogicServices(this IServiceCollection services)
		{
			services.AddScoped<IPreferenceService, PreferenceService>();
			services.AddScoped<IPromocodeService, PromocodeService>();
			services.AddScoped<ICustomerService, CustomerService>();
		}

		private static void AddGivingToCustomerRepositories(this IServiceCollection services)
		{
			services.AddScoped<IMongoDbInitializer, MongoDbInitializer>();

			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IPreferenceRepository, PreferenceRepository>();
			services.AddScoped<IPromoodeRepository, PromoodeRepository>();
		}

		private static void AddGivingToCustomerNotificationServices(this IServiceCollection services)
			=> services.AddScoped<INotificationGateway, NotificationGateway>();
	}
}
