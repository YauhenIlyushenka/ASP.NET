using MassTransit;
using Microsoft.Extensions.Logging;
using Pcf.GivingToCustomer.BLL.Models;
using Pcf.GivingToCustomer.BLL.Services;
using Pcf.GivingToCustomer.Core.Domain.Enums;
using Pcf.MassTransitContracts.Messages;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration.RabbitMQ.Consumers
{
	public class NotifyAboutGivingPromocodeConsumer : IConsumer<PromocodeNotificationMessage>
	{
		private readonly IPromocodeService _promocodeService;
		private readonly ILogger<NotifyAboutGivingPromocodeConsumer> _logger;

		public NotifyAboutGivingPromocodeConsumer(
			IPromocodeService promocodeService,
			ILogger<NotifyAboutGivingPromocodeConsumer> logger)
		{
			_promocodeService = promocodeService;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<PromocodeNotificationMessage> context)
		{
			var modelContract = context.Message;
			_logger.LogInformation($"The message from the queue with promocode: {modelContract.PromoCode} has been received.");

			var dto = new GivePromoCodeRequestDto
			{
				PromoCode = modelContract.PromoCode,
				ServiceInfo = modelContract.ServiceInfo,
				PartnerId = modelContract.PartnerId,
				BeginDate = modelContract.BeginDate,
				EndDate = modelContract.EndDate,
				Preference = (Preference)modelContract.PreferenceId,
				PartnerManagerId = modelContract.PartnerManagerId,
			};

			await _promocodeService.GivePromoCodesToCustomersWithPreferenceAsync(dto);
			_logger.LogInformation($"Giving promocode: {dto.PromoCode} has been completed successfully.");
		}
	}
}
