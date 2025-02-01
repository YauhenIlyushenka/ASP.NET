using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.RabbitMQ.Model;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure.RabbitMQ
{
	public static class MassTransitConfigurator
	{
		public static void ConfigureMassTransit(this IServiceCollection services, RabbitMQSettings rabbitMQSettings)
		{
			services.AddMassTransit(x =>
			{
				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.VHost, c =>
					{
						c.Username(rabbitMQSettings.Login);
						c.Password(rabbitMQSettings.Password);
					});

					cfg.ClearSerialization();
					cfg.UseRawJsonSerializer();

					cfg.ConfigureEndpoints(context);
				});
			});
		}
	}
}