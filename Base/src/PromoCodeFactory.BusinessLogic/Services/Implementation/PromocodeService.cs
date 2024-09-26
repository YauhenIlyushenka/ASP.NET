using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class PromocodeService : BaseService, IPromocodeService
	{
		private readonly IRepository<PromoCode, Guid> _promocodeRepository;
		private readonly IRepository<Preference, Guid> _preferenceRepository;
		private readonly IRepository<Employee, Guid> _employeeRepository;

		public PromocodeService(
			IRepository<PromoCode, Guid> promocodeRepository,
			IRepository<Preference, Guid> preferenceRepository,
			IRepository<Employee, Guid> employeeRepository)
		{
			_promocodeRepository = promocodeRepository;
			_preferenceRepository = preferenceRepository;
			_employeeRepository = employeeRepository;
		}

		public async Task<List<PromoCodeShortResponseDto>> GetAllAsync()
			=> (await _promocodeRepository.GetAllAsync(asNoTracking: true)).Select(x => new PromoCodeShortResponseDto
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
			var employee = await _employeeRepository.GetByIdAsync(x => x.Id.Equals(request.EmployeeId))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(request.EmployeeId, nameof(Employee)));

			var preference = (await _preferenceRepository.GetAllAsync(x => x.Name.Equals(request.Preference.ToString()), nameof(Preference.Customers))).Single();

			var tasks = preference.Customers.Select(async customer =>
			{
				await _promocodeRepository.AddAsync(new PromoCode
				{
					ServiceInfo = request.ServiceInfo,
					PartnerName = request.PartnerName,
					Code = request.PromoCode,
					BeginDate = request.BeginDate.ToDateTime(),
					EndDate = request.EndDate.ToDateTime(),
					Preference = preference,
					PartnerManager = employee,
					Customer = customer,
				});
			});

			await Task.WhenAll(tasks);
			await _promocodeRepository.SaveChangesAsync();
		}
	}
}