using System;
using System.Collections.Generic;
using System.Linq;
using Pcf.Administration.Core.Domain.Administration;
using Pcf.Administration.Core.Helpers;
using EnumRole = Pcf.Administration.Core.Domain.Enums.Role;

namespace Pcf.Administration.IntegrationTests.Data
{
	public static class TestDataFactory
	{
		public static List<Employee> Employees => new List<Employee>()
		{
			new Employee()
			{
				Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
				Email = "owner@somemail.ru",
				FirstName = "Ivan",
				LastName = "Sergeev",
				RoleId = Roles.First().Id,
				Role = Roles
					.Where(x => x.Id == (int)EnumRole.Admin)
					.Select(x => new NestedRole
					{
						Name = x.Name,
						Description = x.Description
					})
					.FirstOrDefault(),
				AppliedPromocodesCount = 5,
			},
			new Employee()
			{
				Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
				Email = "andreev@somemail.ru",
				FirstName = "Petr",
				LastName = "Andreev",
				RoleId = Roles.Last().Id,
				Role = Roles
					.Where(x => x.Id == (int)EnumRole.PartnerManager)
					.Select(x => new NestedRole
					{
						Name = x.Name,
						Description = x.Description
					})
					.FirstOrDefault(),
				AppliedPromocodesCount = 10,
			},
		};

		public static List<GlobalRole> Roles => new List<GlobalRole>()
		{
			new GlobalRole()
			{
				Id = (int)EnumRole.Admin,
				Name = EnumRole.Admin.ToString(),
				Description = EnumHelper.GetDescription(EnumRole.Admin),
			},
			new GlobalRole()
			{
				Id = (int)EnumRole.PartnerManager,
				Name = EnumRole.PartnerManager.ToString(),
				Description = EnumHelper.GetDescription(EnumRole.PartnerManager),
			}
		};
	}
}