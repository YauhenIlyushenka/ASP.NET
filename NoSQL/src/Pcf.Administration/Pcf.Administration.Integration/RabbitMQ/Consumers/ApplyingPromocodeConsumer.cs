using MassTransit;
using Microsoft.Extensions.Logging;
using Pcf.Administration.BLL.Services;
using Pcf.MassTransitContracts.Messages;
using System.Threading.Tasks;

namespace Pcf.Administration.Integration.RabbitMQ.Consumers
{
	public class ApplyingPromocodeConsumer : IConsumer<PromocodeNotificationMessage>
	{
		private readonly IEmployeeService _employeeService;
		private readonly ILogger<ApplyingPromocodeConsumer> _logger;

		public ApplyingPromocodeConsumer(
			IEmployeeService employeeService,
			ILogger<ApplyingPromocodeConsumer> logger)
		{
			_logger = logger;
			_employeeService = employeeService;
		}

		public async Task Consume(ConsumeContext<PromocodeNotificationMessage> context)
		{
			var modelContract = context.Message;
			_logger.LogInformation(string.Format(
				"The message from the queue with promocode: {0} and PartnerManagerId: {1} has been received.",
				modelContract.PromoCode,
				modelContract.PartnerManagerId.HasValue 
					? modelContract.PartnerManagerId.Value 
					: string.Empty));

			if (modelContract.PartnerManagerId.HasValue)
			{
				await _employeeService.UpdateAppliedPromocodesAsync(modelContract.PartnerManagerId.Value);
			}

			_logger.LogInformation(modelContract.PartnerManagerId.HasValue 
				? $"Applied promocode for PartnerManagerId: {modelContract.PartnerManagerId} has been completed successfully."
				: "The PartnerManagerId did not exist in the message.");
		}
	}
}
