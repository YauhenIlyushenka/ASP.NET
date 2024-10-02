﻿using PromoCodeFactory.Core.Domain.Administration;
using System;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
	public class PromoCode : IEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string ServiceInfo { get; set; }
		public DateTime BeginDate { get; set; }
		public DateTime EndDate { get; set; }
		public string PartnerName { get; set; }

		public Guid EmployeeId { get; set; }
		public Employee PartnerManager { get; set; }

		public Guid PreferenceId { get; set; }
		public Preference Preference { get; set; }

		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; }
	}
}
