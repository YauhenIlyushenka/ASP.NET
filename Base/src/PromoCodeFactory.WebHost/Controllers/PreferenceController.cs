using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Response.Preference;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
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
		/// <returns></returns>S
		[HttpGet]
		public async Task<List<PreferenceResponse>> GetPreferencesAsync()
		{
			var preferences = await _preferenceService.GetAllAsync();

			return preferences.Select(x => new PreferenceResponse
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList(); ;
		}
	}
}
