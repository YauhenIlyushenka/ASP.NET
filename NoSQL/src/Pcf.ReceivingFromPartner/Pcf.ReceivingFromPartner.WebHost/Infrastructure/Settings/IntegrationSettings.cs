using Pcf.ReceivingFromPartner.WebHost.Infrastructure.RabbitMQ.Model;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure.Settings
{
	public class IntegrationSettings
	{
		public string CommonDataApiUrl { get; set; }
		public RabbitMQSettings RabbitMQ { get; set; }
	}
}
