using System;
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
	}
}