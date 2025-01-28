using Microsoft.AspNetCore.Mvc;
using Pcf.ReceivingFromPartner.Business.Models;
using Pcf.ReceivingFromPartner.Business.Models.Partner;
using Pcf.ReceivingFromPartner.Business.Services;
using Pcf.ReceivingFromPartner.WebHost.Models.Request;
using Pcf.ReceivingFromPartner.WebHost.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.WebHost.Controllers
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
		/// Get partner by Id
		/// </summary>
		/// <param name="id">Partner Id, example: <example>20d2d612-db93-4ed5-86b1-ff2413bca655</example></param>
		[HttpGet("{id:guid}")]
		public async Task<PartnerResponse> GetPartnerByIdAsync(Guid id)
		{
			var partner = await _partnerService.GetByIdAsync(id);

			var response = new PartnerResponse()
			{
				Id = partner.Id,
				Name = partner.Name,
				NumberIssuedPromoCodes = partner.NumberIssuedPromoCodes,
				IsActive = true,
				PartnerLimits = partner.PartnerLimits
					.Select(y => new PartnerPromoCodeLimitResponse()
					{
						Id = y.Id,
						PartnerId = y.PartnerId,
						Limit = y.Limit,
						CreateDate = y.CreateDate,
						EndDate = y.EndDate,
						CancelDate = y.CancelDate,
					}).ToList()
			};

			return response;
		}

		/// <summary>
		/// Get all partnerLimits of definite partner
		/// </summary>
		/// <param name="id">partner Id, example: <example>20d2d612-db93-4ed5-86b1-ff2413bca655</example></param>
		/// <param name="limitId">limit Id of partner, example: <example>93f3a79d-e9f9-47e6-98bb-1f618db43230</example></param>
		/// <returns></returns>
		[HttpGet("{id:guid}/limits/{limitId:guid}")]
		public async Task<PartnerPromoCodeLimitResponse> GetPartnerLimitAsync(
			[FromRoute] Guid id,
			[FromRoute] Guid limitId)
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
		public async Task<PartnerPromoCodeLimitResponse> SetPartnerPromoCodeLimitAsync(
			[FromRoute] Guid id,
			[FromBody] SetPartnerPromoCodeLimitRequest request)
		{
			var partnerLimit = await _partnerService.SetPartnerPromoCodeLimitAsync(
				id,
				new PartnerPromoCodeLimitRequestDto
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
		/// <param name="id">Partner Id, example: <example>0da65561-cf56-4942-bff2-22f50cf70d43</example></param>
		[HttpPut("{id:guid}/canceledLimits")]
		public async Task CancelPartnerPromoCodeLimitAsync([FromRoute] Guid id)
			=> await _partnerService.CancelPartnerPromoCodeLimitAsync(id);

		/// <summary>
		/// Get promocodes by PartnerId
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}/promocodes")]
		public async Task<List<PromoCodeShortResponse>> GetPartnerPromoCodesAsync([FromRoute] Guid id)
		{
			var promocodes = await _partnerService.GetPromoCodesByPartnerIdAsync(id);

			return promocodes.Select(x => new PromoCodeShortResponse
			{
				Id = x.Id,
				Code = x.Code,
				BeginDate = x.BeginDate,
				EndDate = x.EndDate,
				PartnerId = x.PartnerId,
				PartnerName = x.PartnerName,
				ServiceInfo = x.ServiceInfo,
			}).ToList();
		}

		/// <summary>
		/// Get promocode by PartnerId and PromoCodeId
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id:guid}/promocodes/{promoCodeId:guid}")]
		public async Task<PromoCodeShortResponse> GetPartnerPromoCodeAsync(
			[FromRoute] Guid id,
			[FromRoute] Guid promoCodeId)
		{
			var promocode = await _partnerService.GetPartnerPromoCodeAsync(id, promoCodeId);

			return new PromoCodeShortResponse
			{
				Id = promocode.Id,
				Code = promocode.Code,
				BeginDate = promocode.BeginDate,
				EndDate = promocode.EndDate,
				PartnerId = promocode.PartnerId,
				PartnerName = promocode.PartnerName,
				ServiceInfo = promocode.ServiceInfo,
			};
		}

		/// <summary>
		/// Create promocode from Partner
		/// </summary>
		/// <param name="id">Partner Id, example: <example>20d2d612-db93-4ed5-86b1-ff2413bca655</example></param>
		/// <param name="request">Data of request/example></param>
		/// <returns></returns>
		[HttpPost("{id:guid}/promocodes")]
		public async Task ReceivePromoCodeFromPartnerWithPreferenceAsync(
			[FromRoute] Guid id,
			[FromBody] ReceivingPromoCodeRequest request)
			=> await _partnerService.ReceivePromoCodeFromPartnerWithPreferenceAsync(
				id,
				new ReceivingPromoCodeRequestDto
				{
					PromoCode = request.PromoCode,
					Preference = request.Preference,
					ServiceInfo = request.ServiceInfo,
					PartnerManagerId = request.PartnerManagerId,
				});
	}
}
