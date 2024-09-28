using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.BusinessLogic.Models.PromoCode;
using PromoCodeFactory.BusinessLogic.Services;
using PromoCodeFactory.WebHost.Models.Request.PromoCode;
using PromoCodeFactory.WebHost.Models.Response.PromoCode;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Controllers
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
				PartnerName = x.PartnerName,
				ServiceInfo = x.ServiceInfo
			}).ToList();
		}

		/// <summary>
		/// Give promocode to customers with definite preference
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task GivePromoCodesToCustomersWithPreferenceAsync([FromBody] GivePromoCodeRequest request)
		{
			await _promocodeService.GivePromoCodesToCustomersWithPreferenceAsync(new GivePromoCodeRequestDto
			{
				ServiceInfo = request.ServiceInfo,
				PartnerName = request.PartnerName,
				PromoCode = request.PromoCode,
				BeginDate = request.BeginDate,
				EndDate = request.EndDate,
				EmployeeId = request.EmployeeId,
				Preference = request.Preference,
			});
		}
	}
}
