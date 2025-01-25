using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.Core.Helpers;
using Pcf.ReceivingFromPartner.Integration.Dto;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public class GivingPromoCodeToCustomerGateway : ErrorHandlingGateway, IGivingPromoCodeToCustomerGateway
	{
		private readonly HttpClient _httpClient;

		public GivingPromoCodeToCustomerGateway(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		
		public async Task GivePromoCodeToCustomer(PromoCode promoCode)
		{
			var dto = new GivePromoCodeToCustomerDto()
			{
				PartnerId = promoCode.Partner.Id,
				BeginDate = promoCode.BeginDate.ToDateString(),
				EndDate = promoCode.EndDate.ToDateString(),
				Preference = (Core.Domain.Enum.Preference)promoCode.PreferenceId,
				PromoCode = promoCode.Code,
				ServiceInfo = promoCode.ServiceInfo,
				PartnerManagerId = promoCode.PartnerManagerId,
			};
			
			var response = await _httpClient.PostAsJsonAsync("api/v1/promocode", dto);

			if (!response.IsSuccessStatusCode)
			{
				await HandlingGatewayErrorResponse(response);
			}
		}
	}
}