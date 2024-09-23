using PromoCodeFactory.BusinessLogic.Models.Customer;
using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Exceptions;

namespace PromoCodeFactory.BusinessLogic.Services.Implementation
{
	public class CustomerService : BaseService, ICustomerService
	{
		private readonly IRepository<Customer, Guid> _customerRepository;

		public CustomerService(IRepository<Customer, Guid> customerRepository)
		{
			_customerRepository = customerRepository;
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
			var customer = await _customerRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PromoCodes = new List<PromoCodeShortResponseDto>
				{

				}
			};
		}

		public async Task<CustomerResponseDto> CreateAsync(CreateOrEditCustomerRequestDto model)
		{
			//var preferences = (await _roleRepository.GetAllAsync())
			//	.Where(preference => model.Preferences.Select(x => x.ToString().ToLower()).Contains(preference.Name.ToLower()))
			//	.ToList();

			var customer = await _customerRepository.AddAsync(new Customer
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				//Preferences 
			});

			await _customerRepository.SaveChangesAsync();

			return new CustomerResponseDto
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PromoCodes = new List<PromoCodeShortResponseDto>
				{

				}
				//Preferences 
			};
		}

		public async Task UpdateAsync(Guid id, CreateOrEditCustomerRequestDto model)
		{
			var customer = await _customerRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			//var role = (await _roleRepository.GetAllAsync())
			//	.Single(role => role.Name.Equals(model.Role.ToString(), StringComparison.OrdinalIgnoreCase));

			//employee.FirstName = model.FirstName;
			//employee.LastName = model.LastName;
			//employee.Email = model.Email;
			//employee.AppliedPromocodesCount = model.AppliedPromocodesCount;
			//employee.Role = role;

			_customerRepository.Update(customer);
			await _customerRepository.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var customer = await _customerRepository.GetByIdAsync(id)
				?? throw new NotFoundException(FormatFullNotFoundErrorMessage(id, nameof(Customer)));

			_customerRepository.Delete(customer);
			await _customerRepository.SaveChangesAsync();
		}
	}
}
