using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PromocodeService : IPromocodeService
	{
		private readonly IRepository<PromoCode, Guid> _promocodeRepository;

		public PromocodeService(IRepository<PromoCode, Guid> promocodeRepository)
		{
			_promocodeRepository = promocodeRepository;
		}

		public async Task<List<PromoCodeShortResponseDto>> GetAllAsync()
			=> (await _promocodeRepository.GetAllAsync()).Select(x => new PromoCodeShortResponseDto
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate.ToDateString(),
				EndDate = x.EndDate.ToDateString(),
				PartnerName = x.PartnerName,
				ServiceInfo = x.ServiceInfo
			}).ToList();


		public async Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequestDto request)
		{
			throw new NotImplementedException();
		}
	}
}