using Pcf.ReceivingFromPartner.Core.Domain;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Core.Abstractions.Gateways
{
	public interface IGivingPromoCodeToCustomerGateway
	{
		Task GivePromoCodeToCustomer(PromoCode promoCode);
	}
}