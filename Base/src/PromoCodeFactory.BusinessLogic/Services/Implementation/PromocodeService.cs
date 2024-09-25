using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PromocodeService : IPromocodeService
	{
		private readonly IRepository<PromoCode, Guid> _promocodeRepository;
		private readonly IRepository<Preference, Guid> _preferenceRepository;

		public PromocodeService(
			IRepository<PromoCode, Guid> promocodeRepository,
			IRepository<Preference, Guid> preferenceRepository)
		{
			_promocodeRepository = promocodeRepository;
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<PromoCodeShortResponseDto>> GetAllAsync()
			=> (await _promocodeRepository.GetAllAsync()).Select(x => new PromoCodeShortResponseDto
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate.ToDateString(),
				EndDate = x.EndDate.ToDateString(),
				PartnerName = x.PartnerName,
				ServiceInfo = x.ServiceInfo,
			}).ToList();


		public async Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequestDto request)
		{
			var preference = await _preferenceRepository
				.GetAllAsync(x => x.Name.Equals(request.Preference.ToString()), nameof(Preference.Customers));

			await _promocodeRepository.AddAsync(new PromoCode
			{
				ServiceInfo = request.ServiceInfo,
			});


		}
	}
}