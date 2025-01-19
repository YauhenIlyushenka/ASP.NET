using Pcf.CommonData.Business.Models;
using Pcf.CommonData.Core.Core.Abstractions;
using Pcf.CommonData.Core.Domain;
using Pcf.CommonData.Core.Exceptions;

namespace Pcf.CommonData.Business.Services.Implementation
{
	public class PreferenceService : IPreferenceService
	{
		private readonly IMongoRepository<Preference, int> _preferenceRepository;

		public PreferenceService(IMongoRepository<Preference, int> preferenceRepository)
		{
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<PreferenceResponseDto>> GetAllAsync()
			=> (await _preferenceRepository.GetAllAsync())
				.Select(x => new PreferenceResponseDto
				{
					Id = x.Id,
					Name = x.Name,
				}).ToList();

		public async Task<PreferenceResponseDto> GetPreferenceById(int id)
		{
			var preference = await _preferenceRepository.GetByIdAsync(id)
				?? throw new NotFoundException($"The Preference with Id {id} has not been found.");

			return new PreferenceResponseDto
			{
				Id = preference.Id,
				Name = preference.Name,
			};
		}
	}
}
