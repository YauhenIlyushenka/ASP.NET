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

		//[HttpPost("{id:guid}/limits")]
		//public async Task<IActionResult> SetPartnerPromoCodeLimitAsync([FromRoute] Guid id, [FromBody] SetPartnerPromoCodeLimitRequest request)
		//{
		//	var result = await _partnerService.SetPartnerPromoCodeLimitAsync(id, new PartnerPromoCodeLimitRequestDto
		//	{
		//		EndDate = request.EndDate,
		//		Limit = request.Limit,
		//	});
		//	var partner = await _partnersRepository.GetByIdAsync(id);

		//	if (partner == null)
		//		return NotFound();

		//	//Если партнер заблокирован, то нужно выдать исключение
		//	if (!partner.IsActive)
		//		return BadRequest("Данный партнер не активен");

		//	//Установка лимита партнеру
		//	var activeLimit = partner.PartnerLimits.FirstOrDefault(x =>
		//		!x.CancelDate.HasValue);

		//	if (activeLimit != null)
		//	{
		//		//Если партнеру выставляется лимит, то мы 
		//		//должны обнулить количество промокодов, которые партнер выдал, если лимит закончился, 
		//		//то количество не обнуляется
		//		partner.NumberIssuedPromoCodes = 0;

		//		//При установке лимита нужно отключить предыдущий лимит
		//		activeLimit.CancelDate = DateTime.Now;
		//	}

		//	var newLimit = new PartnerPromoCodeLimit()
		//	{
		//		Limit = request.Limit,
		//		Partner = partner,
		//		PartnerId = partner.Id,
		//		CreateDate = DateTime.Now,
		//		EndDate = request.EndDate
		//	};

		//	partner.PartnerLimits.Add(newLimit);

		//	await _partnersRepository.UpdateAsync(partner);

		//	return CreatedAtAction(nameof(GetPartnerLimitAsync), new { id = partner.Id, limitId = newLimit.Id }, null);
		//}

		//[HttpPost("{id:guid}/canceledLimits")]
		//public async Task<IActionResult> CancelPartnerPromoCodeLimitAsync([FromRoute] Guid id)
		//{
		//	var partner = await _partnersRepository.GetByIdAsync(id);

		//	if (partner == null)
		//		return NotFound();

		//	//Если партнер заблокирован, то нужно выдать исключение
		//	if (!partner.IsActive)
		//		return BadRequest("Данный партнер не активен");

		//	//Отключение лимита
		//	var activeLimit = partner.PartnerLimits.FirstOrDefault(x =>
		//		!x.CancelDate.HasValue);

		//	if (activeLimit != null)
		//	{
		//		activeLimit.CancelDate = DateTime.Now;
		//	}

		//	await _partnersRepository.UpdateAsync(partner);

		//	return NoContent();
		//}
	}
}
