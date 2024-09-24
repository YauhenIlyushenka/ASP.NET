using PromoCodeFactory.BusinessLogic.Models.Preference;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PreferenceService : IPreferenceService
	{
		private readonly IRepository<Preference, Guid> _preferenceRepository;

		public PreferenceService(IRepository<Preference, Guid> preferenceRepository)
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
