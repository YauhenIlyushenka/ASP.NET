using Pcf.GivingToCustomer.BLL.Models;

namespace Pcf.GivingToCustomer.BLL.Services
{
	public interface ICustomerService
	{
		Task<List<CustomerShortResponseDto>> GetAllAsync();
		Task<CustomerResponseDto> GetByIdAsync(Guid id);
		Task<CustomerResponseDto> CreateAsync(CreateOrEditCustomerRequestDto model);
		Task UpdateAsync(Guid id, CreateOrEditCustomerRequestDto model);
		Task DeleteAsync(Guid id);
	}
}
