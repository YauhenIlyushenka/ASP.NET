﻿using System;
using System.Collections.Generic;
using System.Linq;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManagement;
using PromoCodeFactory.Core.Helpers;
using EnumRole = PromoCodeFactory.Core.Domain.Enums.Role;
using PreferenceRole = PromoCodeFactory.Core.Domain.Enums.Preference;

namespace PromoCodeFactory.DataAccess.Data
{
	public static class FakeDataFactory
	{
		public static IList<Employee> Employees => new List<Employee>()
		{
			new Employee()
			{
				Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
				Email = "owner@somemail.ru",
				FirstName = "Ivan",
				LastName = "Sergeev",
				AppliedPromocodesCount = 5,
				RoleId = Roles.First().Id,
			},
			new Employee()
			{
				Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
				Email = "andreev@somemail.ru",
				FirstName = "Petr",
				LastName = "Andreev",
				AppliedPromocodesCount = 10,
				RoleId = Roles.Last().Id,
			},
		};

		public static IList<Role> Roles => new List<Role>()
		{
			new Role()
			{
				Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
				Name = EnumRole.Admin.ToString(),
				DescriptionRole = EnumHelper.GetDescription(EnumRole.Admin),
			},
			new Role()
			{
				Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
				Name = EnumRole.PartnerManager.ToString(),
				DescriptionRole = EnumHelper.GetDescription(EnumRole.PartnerManager),
			}
		};

		public static IList<Preference> Preferences => new List<Preference>()
		{
			new Preference()
			{
				Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
				Name = PreferenceRole.Theater.ToString(),
			},
			new Preference()
			{
				Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
				Name = PreferenceRole.Family.ToString(),
			},
			new Preference()
			{
				Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
				Name = PreferenceRole.Children.ToString(),
			}
		};

		public static IList<Customer> Customers => new List<Customer>
		{
			new Customer()
			{
				Id = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"),
				Email = "ivan_sergeev@mail.ru",
				FirstName = "Ivan",
				LastName = "Petrov",
				Preferences = Preferences,
			}
		};

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
						EndDate = new DateTime(2020, 9, 9).ToUniversalTime(),
						Limit = 100,
					}
				}
			},
		};
	}
}