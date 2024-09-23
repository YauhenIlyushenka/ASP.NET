using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Request.Customer;
using PromoCodeFactory.WebHost.Models.Response.Customer;
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

		[HttpGet("{id:guid}")]
		public async Task<CustomerResponse> GetCustomerByIdAsync([FromRoute] Guid id)
		{
			//TODO: Добавить получение клиента вместе с выданными ему промомкодами
			var customer = await _customerService.GetByIdAsync(id);
			var customerModel = new CustomerResponse
			{
				Id = customer.Id,
				FirstName = customer.FirstName,
				LastName = customer.LastName,
				Email = customer.Email,
				//PromoCodes
			};

			return customerModel;
		}

		[HttpGet]
		public async Task<List<CustomerShortResponse>> GetCustomersAsync()
		{
			var customers = await _customerService.GetAllAsync();
			var customersModels = customers.Select(x => new CustomerShortResponse
			{
				Id = x.Id,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
			}).ToList();

			return customersModels;
		}

		[HttpPost]
		public async Task CreateCustomerAsync([FromBody] CreateOrEditCustomerRequest request)
		{
			//TODO: Добавить создание нового клиента вместе с его предпочтениями
			throw new NotImplementedException();
		}

		[HttpPut("{id:guid}")]
		public async Task UpdateCustomerAsync([FromRoute] Guid id, [FromBody] CreateOrEditCustomerRequest request)
		{
			//TODO: Обновить данные клиента вместе с его предпочтениями
			throw new NotImplementedException();
		}

		[HttpDelete("{id:guid}")]
		public async Task DeleteCustomer([FromRoute] Guid id)
		{
			//TODO: Удаление клиента вместе с выданными ему промокодами
			throw new NotImplementedException();
		}
	}
}
