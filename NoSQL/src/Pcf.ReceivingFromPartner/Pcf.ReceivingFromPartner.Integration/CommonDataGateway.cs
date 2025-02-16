﻿using Newtonsoft.Json;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public class CommonDataGateway : ErrorHandlingGateway, ICommonDataGateway
	{
		private readonly HttpClient _httpClient;

		public CommonDataGateway(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Preference>> GetAllPreferencesAsync()
		{
			var response = await _httpClient.GetAsync("api/v1/preference");
			if (!response.IsSuccessStatusCode)
			{
				await HandlingGatewayErrorResponse(response);
			}

			string responseContent = await response.Content.ReadAsStringAsync();
			var preferences = JsonConvert.DeserializeObject<List<Preference>>(responseContent);

			return preferences;
		}

		public async Task<Preference> GetPreferenceByIdAsync(int id)
		{
			var response = await _httpClient.GetAsync($"api/v1/preference/{id}");
			if (!response.IsSuccessStatusCode)
			{
				await HandlingGatewayErrorResponse(response);
			}

			string responseContent = await response.Content.ReadAsStringAsync();
			var preference = JsonConvert.DeserializeObject<Preference>(responseContent);

			return preference;
		}
	}
}
