using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public class AdministrationGateway : ErrorHandlingGateway, IAdministrationGateway
	{
		private readonly HttpClient _httpClient;

		public AdministrationGateway(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		
		public async Task NotifyAdminAboutPartnerManagerPromoCode(Guid partnerManagerId)
		{
			var response = await _httpClient.PostAsync(
				$"api/v1/employee/{partnerManagerId}/applied-promocodes", 
				new StringContent(string.Empty));
			
			if (!response.IsSuccessStatusCode)
			{
				await HandlingGatewayErrorResponse(response);
			}
		}
	}
}