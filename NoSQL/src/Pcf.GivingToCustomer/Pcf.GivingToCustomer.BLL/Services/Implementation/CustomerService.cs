using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Pcf.GivingToCustomer.BLL.Models;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Core.Exceptions;
using Pcf.GivingToCustomer.Core.Helpers;

namespace Pcf.GivingToCustomer.BLL.Services.Implementation
{
	public class CustomerService : BaseService, ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IPromoodeRepository _promocodeRepository;
		private readonly IPreferenceRepository _preferenceRepository;

		public CustomerService(
			ICustomerRepository customerRepository,
			IPromoodeRepository promocodeRepository,
			IPreferenceRepository preferenceRepository)
		{
			_customerRepository = customerRepository;
			_preferenceRepository = preferenceRepository;
			_promocodeRepository = promocodeRepository;
		}

		public async Task<List<CustomerShortResponseDto>> GetAllAsync()
			=> (await _customerRepository.GetAllAsync()).Select(x => new CustomerShortResponseDto
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
			}).ToList();

		public async Task<CustomerResponseDto> GetByIdAsync(Guid id)
		{
			var customerBsonDocument = await _customerRepository.GetCustomerWithPreferencesAndPromocodes(id);
			if (customerBsonDocument == null)
			{
				throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));
			}

			var customer = BsonSerializer.Deserialize<Customer>(customerBsonDocument);
			var preferences = customerBsonDocument["Preferences"].AsBsonArray
				.Select(b => BsonSerializer.Deserialize<Preference>(b.AsBsonDocument))
				.ToList();

			var promoCodes = customerBsonDocument["PromoCodes"].AsBsonArray
				.Select(b => BsonSerializer.Deserialize<PromoCode>(b.AsBsonDocument))
				.ToList();

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				Preferences = preferences.Select(p => new PreferenceResponseDto
				{
					Id = p.Id,
					Name = p.Name
				}).ToList(),
				PromoCodes = promoCodes.Select(p => new PromoCodeShortResponseDto
				{
					Id = p.Id,
					Code = p.Code,
					BeginDate = p.BeginDate.ToDateString(),
					EndDate = p.EndDate.ToDateString(),
					ServiceInfo = p.ServiceInfo,
					PartnerId = p.PartnerId
				}).ToList()
			};
		}

		public async Task<CustomerResponseDto> CreateAsync(CreateOrEditCustomerRequestDto model)
		{
			var preferenceIds = model.Preferences.Select(x => (int)x);
			var preferences = await _preferenceRepository.GetPreferencesByIds(preferenceIds.ToList());

			var customer = new Customer
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				PreferenceIds = preferenceIds.ToList(),
				PromoCodeIds = [],
			};

			await _customerRepository.AddAsync(customer);
			await _preferenceRepository.UpdateCustomerIdsInPreferences(customer.Id, preferences);

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				Preferences = preferences.Select(p => new PreferenceResponseDto
				{
					Id = p.Id,
					Name = p.Name
				}).ToList(),
			};
		}

		public async Task UpdateAsync(Guid id, CreateOrEditCustomerRequestDto model)
		{
			var updatePreferenceIds = model.Preferences.Select(x => (int)x);
			var updatePreferences = await _preferenceRepository.GetPreferencesByIds(updatePreferenceIds.ToList());

			var customer = await _customerRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			var oldPreferenceIds = customer.PreferenceIds.ToList();

			customer.FirstName = model.FirstName;
			customer.LastName = model.LastName;
			customer.Email = model.Email;
			customer.PreferenceIds = updatePreferenceIds.ToList();

			await _customerRepository.UpdateAsync(id, customer);
			await _preferenceRepository.UpdateCustomerIdsInPreferences(id, updatePreferences, oldPreferenceIds);
		}

		public async Task DeleteAsync(Guid id)
		{
			var customer = await _customerRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			var promocodes = await _promocodeRepository.GetPromocodesByIds(customer.PromoCodeIds.ToList());
			if (promocodes != null && promocodes.Any())
			{
				await _promocodeRepository.DeleteManyByIdsAsync(promocodes.Select(x => x.Id));
			}

			var preferences = await _preferenceRepository.GetPreferencesByIds(customer.PreferenceIds.ToList());

			await _preferenceRepository.UpdatePreferencesByRemovingCustomer(preferences, id, customer.PromoCodeIds.ToList());
			await _customerRepository.DeleteAsync(id);
		}
	}
}
