using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.WebHost.Models.Request.Customer;
using PromoCodeFactory.WebHost.Models.Response.Customer;
using System.Threading.Tasks;
using System;

namespace PromoCodeFactory.WebHost.Controllers
{
	/// <summary>
	/// Customer
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CustomerController
	{
		[HttpGet]
		public Task<CustomerShortResponse> GetCustomersAsync()
		{
			//TODO: Добавить получение списка клиентов
			throw new NotImplementedException();
		}

		[HttpGet("{id:guid}")]
		public Task<CustomerResponse> GetCustomerAsync(Guid id)
		{
			//TODO: Добавить получение клиента вместе с выданными ему промомкодами
			throw new NotImplementedException();
		}

		[HttpPost]
		public Task CreateCustomerAsync(CreateOrEditCustomerRequest request)
		{
			//TODO: Добавить создание нового клиента вместе с его предпочтениями
			throw new NotImplementedException();
		}

		[HttpPut("{id:guid}")]
		public Task EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
		{
			//TODO: Обновить данные клиента вместе с его предпочтениями
			throw new NotImplementedException();
		}

		[HttpDelete]
		public Task DeleteCustomer(Guid id)
		{
			//TODO: Удаление клиента вместе с выданными ему промокодами
			throw new NotImplementedException();
		}
	}
}
