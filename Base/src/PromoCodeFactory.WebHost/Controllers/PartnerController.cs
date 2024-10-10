using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.WebHost.Models.Response.Partner;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PromoCodeFactory.BusinessLogic.Services;
using System.Linq;
using PromoCodeFactory.WebHost.Models.Request.Partner;
using PromoCodeFactory.BusinessLogic.Models.Partner;

namespace PromoCodeFactory.WebHost.Controllers
{
	/// <summary>
	/// Partner
	/// </summary>
	[ApiController]
	[Route("api/v1/[controller]")]
	public class PartnerController
	{
		private readonly IPartnerService _partnerService;

		public PartnerController(IPartnerService partnerService)
		{
			_partnerService = partnerService;
		}

		/// <summary>
		/// Get all partners with partnerLimits
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<List<PartnerResponse>> GetPartnersAsync()
		{
			var partners = await _partnerService.GetAllAsync();

			return partners.Select(x => new PartnerResponse
			{
				Id = x.Id,
				Name = x.Name,
				NumberIssuedPromoCodes = x.NumberIssuedPromoCodes,
				IsActive = x.IsActive,
				PartnerLimits = x.PartnerLimits.Select(y => new PartnerPromoCodeLimitResponse
				{
					Id = y.Id,
					PartnerId = y.PartnerId,
					Limit = y.Limit,
					CreateDate = y.CreateDate,
					EndDate = y.EndDate,
					CancelDate = y.CancelDate,
				}).ToList()
			}).ToList();
		}


		/// <summary>
		/// Get all partnerLimits of definite partner
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}/limits/{limitId:guid}")]
		public async Task<PartnerPromoCodeLimitResponse> GetPartnerLimitAsync([FromRoute] Guid id, [FromRoute] Guid limitId)
		{
			var partnerLimit = await _partnerService.GetPartnerLimitAsync(id, limitId);

			return new PartnerPromoCodeLimitResponse()
			{
				Id = partnerLimit.Id,
				PartnerId = partnerLimit.PartnerId,
				Limit = partnerLimit.Limit,
				CreateDate = partnerLimit.CreateDate,
				EndDate = partnerLimit.EndDate,
				CancelDate = partnerLimit.CancelDate,
			};
		}

		/// <summary>
		/// Set PromocodeLimit for definite partner
		/// </summary>
		/// <returns></returns>
		[HttpPost("{id:guid}/limits")]
		public async Task<PartnerPromoCodeLimitResponse> SetPartnerPromoCodeLimitAsync([FromRoute] Guid id, [FromBody] SetPartnerPromoCodeLimitRequest request)
		{
			var partnerLimit = await _partnerService.SetPartnerPromoCodeLimitAsync(id, new PartnerPromoCodeLimitRequestDto
			{
				EndDate = request.EndDate,
				Limit = request.Limit,
			});

			return new PartnerPromoCodeLimitResponse
			{
				Id = partnerLimit.Id,
				PartnerId = partnerLimit.PartnerId,
				Limit = partnerLimit.Limit,
				CreateDate = partnerLimit.CreateDate,
				EndDate = partnerLimit.EndDate,
				CancelDate = partnerLimit.CancelDate,
			};
		}

		/// <summary>
		/// Cancel PromocodeLimit for definite partner
		/// </summary>
		/// <returns></returns>
		[HttpPut("{id:guid}/canceledLimits")]
		public async Task CancelPartnerPromoCodeLimitAsync([FromRoute] Guid id)
		{
			await _partnerService.CancelPartnerPromoCodeLimitAsync(id);
		}
	}
}
