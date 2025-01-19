using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public class AdministrationGateway : IAdministrationGateway
	{
		private readonly HttpClient _httpClient;

		public AdministrationGateway(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		
		public async Task NotifyAdminAboutPartnerManagerPromoCode(Guid partnerManagerId)
		{
			var response = await _httpClient.PostAsync(
				$"api/v1/employee/{partnerManagerId}/appliedPromocodes", 
				new StringContent(string.Empty));

			response.EnsureSuccessStatusCode();
		}
	}
}