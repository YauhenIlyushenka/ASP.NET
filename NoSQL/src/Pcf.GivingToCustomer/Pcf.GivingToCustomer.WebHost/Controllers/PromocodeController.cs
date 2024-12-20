using Microsoft.AspNetCore.Mvc;
using Pcf.GivingToCustomer.BLL.Models;
using Pcf.GivingToCustomer.BLL.Services;
using Pcf.GivingToCustomer.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.WebHost.Controllers
{
	/// <summary>
	/// Promocode
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PromocodeController
	{
		private readonly IPromocodeService _promocodeService;

		public PromocodeController(IPromocodeService promocodeService)
		{
			_promocodeService = promocodeService;
		}

		/// <summary>
		/// Get all promocodes
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<PromoCodeShortResponse>> GetPromocodesAsync()
		{
			var promocodes = await _promocodeService.GetAllAsync();

			return promocodes.Select(x => new PromoCodeShortResponse
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate,
				EndDate = x.EndDate,
				PartnerId = x.PartnerId,
				ServiceInfo = x.ServiceInfo
			}).ToList();
		}

		/// <summary>
		/// Give promocode to customers with definite preference
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task GivePromoCodesToCustomersWithPreferenceAsync(
			[FromBody] GivePromoCodeRequest request)
			=> await _promocodeService.GivePromoCodesToCustomersWithPreferenceAsync(
				new GivePromoCodeRequestDto
				{
					ServiceInfo = request.ServiceInfo,
					PartnerId = request.PartnerId,
					PromoCode = request.PromoCode,
					BeginDate = request.BeginDate,
					EndDate = request.EndDate,
					Preference = request.Preference,
				});
	}
}
