using Microsoft.AspNetCore.Mvc;
using Pcf.ReceivingFromPartner.Business.Services;
using Pcf.ReceivingFromPartner.WebHost.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.WebHost.Controllers
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
