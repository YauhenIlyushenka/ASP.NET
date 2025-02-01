using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Pcf.GivingToCustomer.Integration.RabbitMQ.Consumers;
using Pcf.GivingToCustomer.WebHost.Infrastructure.RabbitMQ.Model;
using Pcf.MassTransitContracts;
using RabbitMQ.Client;
using System;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure.RabbitMQ
{
	public static class MassTransitConfigurator
	{
		public const string GivingToCustomerQueueName = "NotifyAboutGivingPromocodeQueue";
		public const string GivingToCustomerDeadLetterQueueName = "DLQNotifyAboutGivingPromocode";
		
		public static void ConfigureMassTransit(this IServiceCollection services, RabbitMQSettings rabbitMQSettings)
		{
			services.AddMassTransit(x =>
			{
				x.AddConsumer<NotifyAboutGivingPromocodeConsumer>();

				x.UsingRabbitMq((context, cfg) =>
				{
					cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.VHost, c =>
					{
						c.Username(rabbitMQSettings.Login);
						c.Password(rabbitMQSettings.Password);
					});

					cfg.ReceiveEndpoint(GivingToCustomerQueueName, e =>
					{
						e.ConfigureConsumer<NotifyAboutGivingPromocodeConsumer>(context);

						// Binding exchange with routing key
						e.Bind(ExchangeNameConstants.ExchangePromocodeNotificationName, x =>
						{
							x.ExchangeType = ExchangeType.Direct; // Mention type of exchange
							x.RoutingKey = RoutingKeyConstants.PromocodeNotificationKey;  // Mention Routing key
							x.Durable = false;
						});

						// Setup retry
						e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(40)));

						// Setup Dead Letter Queue (DLQ)
						e.BindDeadLetterQueue(
							ExchangeNameConstants.DeadLetterExchange,
							GivingToCustomerDeadLetterQueueName,
							dlq =>
							{
								dlq.ExchangeType = ExchangeType.Direct;
								dlq.RoutingKey = RoutingKeyConstants.PromocodeNotificationKey;
								dlq.Durable = false; // Queues and exchanges doesn't save with restarting RabbitMQ in Docker container
								dlq.AutoDelete = true; // The DLQ queue should be deleted automatically
							});
					});

					cfg.ClearSerialization();
					cfg.UseRawJsonSerializer();

					cfg.ConfigureEndpoints(context);
				});
			});
		}
	}
}
