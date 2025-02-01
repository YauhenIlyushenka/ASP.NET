using Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using System;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration
{
	public class NotificationGateway : INotificationGateway
	{
		public Task SendNotificationToPartnerAsync(Guid partnerId, string message)
		{
			//Код, который вызывает сервис отправки уведомлений партнеру
			
			return Task.CompletedTask;
		}
	}
}