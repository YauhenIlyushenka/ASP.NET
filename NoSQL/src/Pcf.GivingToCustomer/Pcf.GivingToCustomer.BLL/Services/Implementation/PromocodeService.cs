using MongoDB.Driver;
using Pcf.GivingToCustomer.BLL.Models;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Core.Exceptions;
using Pcf.GivingToCustomer.Core.Helpers;

namespace Pcf.GivingToCustomer.BLL.Services.Implementation
{
	public class PromocodeService : BaseService, IPromocodeService
	{
		private readonly IPromoodeRepository _promocodeRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IPreferenceRepository _preferenceRepository;

		public PromocodeService(
			IPromoodeRepository promocodeRepository,
			ICustomerRepository customerRepository,
			IPreferenceRepository preferenceRepository)
		{
			_promocodeRepository = promocodeRepository;
			_customerRepository = customerRepository;
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<PromoCodeShortResponseDto>> GetAllAsync()
			=> (await _promocodeRepository.GetAllAsync()).Select(x => new PromoCodeShortResponseDto
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate.ToDateString(),
				EndDate = x.EndDate.ToDateString(),
				PartnerId = x.PartnerId,
				ServiceInfo = x.ServiceInfo,
			}).ToList();

		public async Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequestDto request)
		{
			var preference = await _preferenceRepository.GetByIdAsync((int)request.Preference)
				?? throw new NotFoundException($"The {nameof(Preference)} with Id {(int)request.Preference} has not been found.");

			var customers = await _customerRepository.GetCustomersWithPreference(preference.Id);

			if (customers == null || !customers.Any())
			{
				throw new NotFoundException($"Customers with this {nameof(Preference)}: {preference.Id} have not been found.");
			}

			var tasks = customers.Select(async customer =>
			{
				var promoCode = new PromoCode
				{
					ServiceInfo = request.ServiceInfo,
					Code = request.PromoCode,
					BeginDate = request.BeginDate.ToDateTime(),
					EndDate = request.EndDate.ToDateTime(),
					PartnerId = request.PartnerId,
					PreferenceId = preference.Id,
					CustomerId = customer.Id,
					PartnerManagerId = request.PartnerManagerId,
				};

				await _promocodeRepository.AddAsync(promoCode);

				return new CustomerPromocodeResult
				{
					PromocodeId = promoCode.Id,
					CustomerId = customer.Id,
				};
			}).ToList();

			var result = await Task.WhenAll(tasks);

			await _customerRepository.UpdateCustomersWithNewPromocode(result.ToList());
			await _preferenceRepository.UpdatePreferenceWithNewPromocodes(preference.Id, result.Select(x => x.PromocodeId).ToList());
		}
	}
}
