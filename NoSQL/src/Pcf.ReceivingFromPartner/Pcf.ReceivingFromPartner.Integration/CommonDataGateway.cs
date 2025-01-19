using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public class CommonDataGateway : ICommonDataGateway
	{
		private readonly HttpClient _httpClient;

		public CommonDataGateway(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Preference>> GetAllPreferencesAsync()
		{
			var response = await _httpClient.GetAsync("api/v1/preference/getpreferencesasync");
			response.EnsureSuccessStatusCode();

			string responseContent = await response.Content.ReadAsStringAsync();

			var preferences = JsonSerializer.Deserialize<List<Preference>>(responseContent);
			return preferences;
		}

		public async Task<Preference> GetPreferenceByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"api/v1/preference/getpreferencebyidasync/{id}");
			response.EnsureSuccessStatusCode();

			string responseContent = await response.Content.ReadAsStringAsync();

			var preference = JsonSerializer.Deserialize<Preference>(responseContent);
			return preference;
		}
	}
}
