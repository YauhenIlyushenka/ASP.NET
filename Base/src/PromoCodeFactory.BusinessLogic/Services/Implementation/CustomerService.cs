using PromoCodeFactory.BusinessLogic.Models.Customer;
using PromoCodeFactory.BusinessLogic.Models.Preference;
using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;
using PromoCodeFactory.Core.Helpers;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class CustomerService : BaseService, ICustomerService
	{
		private readonly IRepository<Customer, Guid> _customerRepository;
		private readonly IRepository<Preference, Guid> _preferenceRepository;

		public CustomerService(
			IRepository<Customer, Guid> customerRepository,
			IRepository<Preference, Guid> preferenceRepository)
		{
			_customerRepository = customerRepository;
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<CustomerShortResponseDto>> GetAllAsync()
			=> (await _customerRepository.GetAllAsync(asNoTracking: true)).Select(x => new CustomerShortResponseDto
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
			}).ToList();

		public async Task<CustomerResponseDto> GetByIdAsync(Guid id)
		{
			var customer = await _customerRepository.GetByIdAsync(x => x.Id.Equals(id), $"{nameof(Customer.PromoCodes)}", asNoTracking: true) 
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PromoCodes = customer.PromoCodes.Select(x => new PromoCodeShortResponseDto
				{
					Id = x.Id,
					Code = x.Code,
					BeginDate = x.BeginDate.ToDateString(),
					EndDate = x.EndDate.ToDateString(),
					PartnerName = x.PartnerName,
					ServiceInfo = x.ServiceInfo,
				}).ToList()
			};
		}

		public async Task<CustomerResponseDto> CreateAsync(CreateOrEditCustomerRequestDto model)
		{
			var enteredPreferences = model.Preferences.Select(x => x.ToString().ToLower());
			var preferences = await _preferenceRepository.GetAllAsync(x => enteredPreferences.Contains(x.Name.ToLower()));

			var customer = await _customerRepository.AddAsync(new Customer
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				Preferences = preferences,
			});

			await _customerRepository.SaveChangesAsync();

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				Preferences = preferences.Select(x => new PreferenceResponseDto
				{
					Id = x.Id,
					Name = x.Name,
				}).ToList()
			};
		}

		public async Task UpdateAsync(Guid id, CreateOrEditCustomerRequestDto model)
		{
			var customer = await _customerRepository.GetByIdAsync(x => x.Id.Equals(id))
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			var enteredPreferences = model.Preferences.Select(x => x.ToString().ToLower());
			var preferences = await _preferenceRepository.GetAllAsync(x => enteredPreferences.Contains(x.Name.ToLower()));

			customer.FirstName = model.FirstName;
			customer.LastName = model.LastName;
			customer.Email = model.Email;
			customer.Preferences = preferences;

			_customerRepository.Update(customer);
			await _customerRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var customer = await _customerRepository.GetByIdAsync(x => x.Id.Equals(id), $"{nameof(Customer.PromoCodes)}")
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			_customerRepository.Delete(customer);
			await _customerRepository.SaveChangesAsync();
		}
	}
}
