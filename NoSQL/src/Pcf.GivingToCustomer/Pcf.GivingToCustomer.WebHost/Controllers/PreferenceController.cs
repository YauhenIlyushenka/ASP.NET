using Microsoft.AspNetCore.Mvc;
using Pcf.GivingToCustomer.BLL.Services;
using Pcf.GivingToCustomer.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.WebHost.Controllers
{
	/// <summary>
	/// Preference
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PreferenceController
	{
		private readonly IPreferenceService _preferenceService;

		public PreferenceController(IPreferenceService preferenceService)
		{
			_preferenceService = preferenceService;
		}

		/// <summary>
		/// Get all existed preferences 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<PreferenceResponse>> GetPreferencesAsync()
		{
			var preferences = await _preferenceService.GetAllAsync();

			return preferences.Select(x => new PreferenceResponse
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
		}
	}
}