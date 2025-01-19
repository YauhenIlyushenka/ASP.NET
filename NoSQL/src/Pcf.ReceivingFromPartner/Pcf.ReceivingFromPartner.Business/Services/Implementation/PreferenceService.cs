using Pcf.ReceivingFromPartner.Business.Models;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.Core.Exceptions;

namespace Pcf.ReceivingFromPartner.Business.Services.Implementation
{
	public class PreferenceService : IPreferenceService
	{
		private readonly ICommonDataGateway _commonDataGateway;

		public PreferenceService(ICommonDataGateway commonDataGateway)
		{
			_commonDataGateway = commonDataGateway;
		}

		public async Task<List<PreferenceResponseDto>> GetAllAsync()
		{
			var result = await _commonDataGateway.GetAllPreferencesAsync();

			return result.Select(x => new PreferenceResponseDto
			{
				Id = x.Id,
				Name = x.Name,
			}).ToList();
		}

		public async Task<PreferenceResponseDto> GetPreferenceByIdAsync(int id)
		{
			var result = await _commonDataGateway.GetPreferenceByIdAsync(id)
				?? throw new NotFoundException($"The {nameof(Preference)} with Id {id} has not been found.");

			return new PreferenceResponseDto
			{
				Id = result.Id,
				Name = result.Name,
			};
		}
	}
}
