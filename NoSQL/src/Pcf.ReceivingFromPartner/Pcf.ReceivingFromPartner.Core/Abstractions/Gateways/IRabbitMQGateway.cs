using Pcf.ReceivingFromPartner.Core.Domain;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Core.Abstractions.Gateways
{
	public interface IRabbitMQGateway
	{
		Task SendNotificationAboutGivingPromocode(PromoCode promoCode);
	}
}
