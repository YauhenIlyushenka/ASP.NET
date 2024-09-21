using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.WebHost.Models.Request.PromoCode;
using PromoCodeFactory.WebHost.Models.Response.PromoCode;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PromoCodeFactory.WebHost.Controllers
{
	/// <summary>
	/// Promocode
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PromocodeController
	{
		/// <summary>
		/// Получить все промокоды
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public Task<List<PromoCodeShortResponse>> GetPromocodesAsync()
		{
			//TODO: Получить все промокоды 
			throw new NotImplementedException();
		}

		/// <summary>
		/// Создать промокод и выдать его клиентам с указанным предпочтением
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public Task GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
		{
			//TODO: Создать промокод и выдать его клиентам с указанным предпочтением
			throw new NotImplementedException();
		}
	}
}
