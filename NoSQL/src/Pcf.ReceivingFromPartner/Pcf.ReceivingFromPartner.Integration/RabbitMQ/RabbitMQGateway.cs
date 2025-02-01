using MassTransit;
using Microsoft.Extensions.Logging;
using Pcf.MassTransitContracts;
using Pcf.MassTransitContracts.Messages;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.Core.Helpers;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMQ
{
	public class RabbitMQGateway : IRabbitMQGateway
	{
		private readonly IPublishEndpoint _endpoint;
		private readonly ILogger<RabbitMQGateway> _logger;

		public RabbitMQGateway(
			IPublishEndpoint endpoint,
			ILogger<RabbitMQGateway> logger)
		{
			_endpoint = endpoint;
			_logger = logger;
		}

		public async Task SendNotificationAboutGivingPromocode(PromoCode promoCode)
		{
			var message = new PromocodeNotificationMessage
			{
				PartnerId = promoCode.Partner.Id,
				BeginDate = promoCode.BeginDate.ToDateString(),
				EndDate = promoCode.EndDate.ToDateString(),
				PreferenceId = promoCode.PreferenceId,
				PromoCode = promoCode.Code,
				ServiceInfo = promoCode.ServiceInfo,
				PartnerManagerId = promoCode.PartnerManagerId,
			};

			await _endpoint.Publish<PromocodeNotificationMessage>(message, x =>
			{
				x.SetRoutingKey(RoutingKeyConstants.PromocodeNotificationKey);
			});

			_logger.LogInformation($"Giving promocode: {message.PromoCode} has been sent in the queue successfully");
		}
	}
}
