using System.Collections.Generic;
using System;

namespace PromoCodeFactory.WebHost.Models.Request.Customer
{
	public class CreateOrEditCustomerRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<Guid> PreferenceIds { get; set; }
	}
}
