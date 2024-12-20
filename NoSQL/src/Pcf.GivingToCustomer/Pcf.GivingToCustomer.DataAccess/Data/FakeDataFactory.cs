using System;
using System.Collections.Generic;
using Pcf.GivingToCustomer.Core.Domain;
using EnumPreference = Pcf.GivingToCustomer.Core.Domain.Enums.Preference;

namespace Pcf.GivingToCustomer.DataAccess.Data
{
	public static class FakeDataFactory
	{
		public static readonly Guid CustomerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
		public static List<Preference> Preferences => new List<Preference>()
		{
			new Preference()
			{
				Id = (int)EnumPreference.Theater,
				Name = EnumPreference.Theater.ToString(),
				CustomerIds = new List<Guid>{ CustomerId },
				PromoCodeIds = [],
			},
			new Preference()
			{
				Id = (int)EnumPreference.Family,
				Name = EnumPreference.Family.ToString(),
				CustomerIds = [],
				PromoCodeIds = [],
			},
			new Preference()
			{
				Id = (int)EnumPreference.Children,
				Name = EnumPreference.Children.ToString(),
				CustomerIds = new List<Guid>{ CustomerId },
				PromoCodeIds = [],
			}
		};

		public static List<Customer> Customers
		{
			get
			{
				var customers = new List<Customer>()
				{
					new Customer()
					{
						Id = CustomerId,
						Email = "ignat_frogovich1177@test.com",
						FirstName = "Ignat",
						LastName = "Frogovich",
						PreferenceIds = new List<int>()
						{
							(int)EnumPreference.Theater,
							(int)EnumPreference.Children,
						},
						PromoCodeIds = [],
					}
				};

				return customers;
			}
		}
	}
}