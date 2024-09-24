using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Models.Customer;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Request.Customer;
using PromoCodeFactory.WebHost.Models.Response.Customer;
using PromoCodeFactory.WebHost.Models.Response.PromoCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
{
	/// <summary>
	/// Customer
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CustomerController
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<List<CustomerShortResponse>> GetCustomersAsync()
		{
			var customers = await _customerService.GetAllAsync();

			return customers.Select(x => new CustomerShortResponse
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
			}).ToList();
		}

		[HttpGet("{id:guid}")]
		public async Task<CustomerResponse> GetCustomerByIdAsync([FromRoute] Guid id)
		{
			var customer = await _customerService.GetByIdAsync(id);

			return new CustomerResponse
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				PromoCodes = customer.PromoCodes?.Select(x => new PromoCodeShortResponse
				{
					Id = x.Id,
					Code = x.Code,
					BeginDate = x.BeginDate,
					EndDate = x.EndDate,
					PartnerName = x.PartnerName,
					ServiceInfo = x.ServiceInfo,
				}).ToList()
			};
		}

		[HttpPost]
		public async Task<CustomerResponse> CreateCustomerAsync([FromBody] CreateOrEditCustomerRequest request)
		{
			var customer = await _customerService.CreateAsync(new CreateOrEditCustomerRequestDto
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Preferences = request.Preferences,
			});

			return new CustomerResponse
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				Preferences = customer.Preferences?.Select(x => new Models.Response.Preference.PreferenceResponse
				{
					Id = x.Id,
					Name = x.Name,
				}).ToList(),
				PromoCodes = customer.PromoCodes?.Select(x => new PromoCodeShortResponse
				{
					Id = x.Id,
					Code = x.Code,
					BeginDate = x.BeginDate,
					EndDate = x.EndDate,
					PartnerName = x.PartnerName,
					ServiceInfo = x.ServiceInfo,
				}).ToList()
			};
		}

		[HttpPut("{id:guid}")]
		public async Task UpdateCustomerAsync([FromRoute] Guid id, [FromBody] CreateOrEditCustomerRequest request)
		{
			await _customerService.UpdateAsync(id, new CreateOrEditCustomerRequestDto
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Preferences = request.Preferences,
			});
		}

		[HttpDelete("{id:guid}")]
		public async Task DeleteCustomer([FromRoute] Guid id)
		{
			//TODO: Удаление клиента вместе с выданными ему промокодами
			await _customerService.DeleteAsync(id);
		}
	}
}
