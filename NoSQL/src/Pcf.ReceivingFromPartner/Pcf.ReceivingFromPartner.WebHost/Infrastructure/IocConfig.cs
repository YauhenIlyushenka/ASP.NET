using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pcf.ReceivingFromPartner.Business.Services;
using Pcf.ReceivingFromPartner.Business.Services.Implementation;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Abstractions.Repositories;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.DataAccess.Repository;
using Pcf.ReceivingFromPartner.Integration;
using Pcf.ReceivingFromPartner.Integration.RabbitMQ;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.Settings;
using System;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddReceivingFromPartnerServices(this IServiceCollection services, IConfiguration configuration)
		{
			AddBussinessLogicServices(services);
			AddDataAccessLayerRepositories(services);
			AddGateways(services, configuration);
		}

		private static void AddBussinessLogicServices(this IServiceCollection services)
		{
			services.AddScoped<IPreferenceService, PreferenceService>();
			services.AddScoped<IPartnerService, PartnerService>();
		}

		private static void AddDataAccessLayerRepositories(this IServiceCollection services)
			=> services.AddScoped<IRepository<Partner, Guid>, PartnerRepository>();

		private static void AddGateways(this IServiceCollection services, IConfiguration configuration)
		{
			// For synchronous flow to another API's via httpClient;
			services.AddHttpClient<ICommonDataGateway, CommonDataGateway>(httpClient =>
			{
				httpClient.BaseAddress = new Uri(configuration
					.GetSection(nameof(IntegrationSettings)).Get<IntegrationSettings>().CommonDataApiUrl);
			});

			//services.AddHttpClient<IGivingPromoCodeToCustomerGateway, GivingPromoCodeToCustomerGateway>(httpClient =>
			//{
			//	httpClient.BaseAddress = new Uri(configuration["IntegrationSettings:GivingToCustomerApiUrl"]);
			//});

			//services.AddHttpClient<IAdministrationGateway, AdministrationGateway>(httpClient =>
			//{
			//	httpClient.BaseAddress = new Uri(configuration["IntegrationSettings:AdministrationApiUrl"]);
			//});

			// For asynchronous flow via MassTransit/RabbitMQ
			services.AddScoped<INotificationGateway, NotificationGateway>();
			services.AddScoped<IRabbitMQGateway, RabbitMQGateway>();
		}
	}
}
