using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pcf.CommonData.Business.Services;
using Pcf.CommonData.Core.Exceptions;
using Pcf.CommonData.WebHost.Models;

namespace Pcf.CommonData.WebHost.Controllers
{
	/// <summary>
	/// Preference
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PreferenceController
	{
		private readonly IPreferenceService _preferenceService;
		private readonly IValidator<int> _validator;

		public PreferenceController(
			IPreferenceService preferenceService,
			IValidator<int> validator)
		{
			_preferenceService = preferenceService;
			_validator = validator;
		}

		/// <summary>
		/// Get all existed preferences 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<PreferenceResponse>> GetPreferencesAsync()
		{
			var result = await _preferenceService.GetAllAsync();

			return result.Select(x => new PreferenceResponse
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
		}

		/// <summary>
		/// Get preference by Id 
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:int}")]
		public async Task<PreferenceResponse> GetPreferenceByIdAsync([FromRoute] int id)
		{
			var validationResult = _validator.Validate(id);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException(string.Join(
					",",
					validationResult.Errors.SelectMany(x => x.ErrorMessage)));
			}

			var preference = await _preferenceService.GetPreferenceById(id);

			return new PreferenceResponse
			{
				Id = preference.Id,
				Name = preference.Name
			};
		}
	}
}
