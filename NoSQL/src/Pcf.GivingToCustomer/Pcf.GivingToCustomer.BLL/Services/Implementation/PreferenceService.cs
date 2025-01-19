using Pcf.GivingToCustomer.BLL.Models;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;

namespace Pcf.GivingToCustomer.BLL.Services.Implementation
{
	public class PreferenceService : IPreferenceService
	{
		private readonly IPreferenceRepository _preferenceRepository;

		public PreferenceService(IPreferenceRepository preferenceRepository)
		{
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<PreferenceResponseDto>> GetAllAsync()
			=> (await _preferenceRepository.GetAllAsync()).Select(x => new PreferenceResponseDto
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
	}
}
