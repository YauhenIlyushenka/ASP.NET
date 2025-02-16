﻿using Pcf.ReceivingFromPartner.Core.Domain;
using System;
using System.Collections.Generic;

namespace Pcf.ReceivingFromPartner.DataAccess.Data
{
	public static class FakeDataFactory
	{
		public static List<Partner> Partners => new List<Partner>()
		{
			new Partner()
			{
				Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
				Name = "Super toys",
				IsActive = true,
				PartnerLimits = new List<PartnerPromoCodeLimit>()
				{
					new PartnerPromoCodeLimit()
					{
						Id = Guid.Parse("e00633a5-978a-420e-a7d6-3e1dab116393"),
						CreateDate = new DateTime(2020,07,9).ToUniversalTime(),
						EndDate = new DateTime(2020,10,9).ToUniversalTime(),
						Limit = 100
					}
				}
			},
			new Partner()
			{
				Id = Guid.Parse("894b6e9b-eb5f-406c-aefa-8ccb35d39319"),
				Name = "Cat for everyone",
				IsActive = true,
				PartnerLimits = new List<PartnerPromoCodeLimit>()
				{
					new PartnerPromoCodeLimit()
					{
						Id = Guid.Parse("c9bef066-3c5a-4e5d-9cff-bd54479f075e"),
						CreateDate = new DateTime(2020,05,3).ToUniversalTime(),
						EndDate = new DateTime(2020,10,15).ToUniversalTime(),
						CancelDate = new DateTime(2020,06,16).ToUniversalTime(),
						Limit = 1000,
					},
					new PartnerPromoCodeLimit()
					{
						Id = Guid.Parse("0e94624b-1ff9-430e-ba8d-ef1e3b77f2d5"),
						CreateDate = new DateTime(2024, 05, 3).ToUniversalTime(),
						EndDate = new DateTime(2025, 10, 15).ToUniversalTime(),
						Limit = 2,
					},
				}
			},
			new Partner()
			{
				Id = Guid.Parse("0da65561-cf56-4942-bff2-22f50cf70d43"),
				Name = "Fish of your dream",
				IsActive = false,
				PartnerLimits = new List<PartnerPromoCodeLimit>()
				{
					new PartnerPromoCodeLimit()
					{
						Id = Guid.Parse("0691bb24-5fd9-4a52-a11c-34bb8bc9364e"),
						CreateDate = new DateTime(2020, 07, 3).ToUniversalTime(),
						EndDate = DateTime.Now.AddMonths(1).ToUniversalTime(),
						Limit = 100 
					}
				}
			},
			new Partner()
			{
				Id = Guid.Parse("20d2d612-db93-4ed5-86b1-ff2413bca655"),
				Name = "PromoTheaters",
				IsActive = false,
				PartnerLimits = new List<PartnerPromoCodeLimit>()
				{
					new PartnerPromoCodeLimit()
					{
						Id = Guid.Parse("93f3a79d-e9f9-47e6-98bb-1f618db43230"),
						CreateDate = new DateTime(2020,09,6).ToUniversalTime(),
						EndDate = DateTime.Now.AddMonths(1).ToUniversalTime(),
						Limit = 15 
					}
				}
			}
		};
	}
}