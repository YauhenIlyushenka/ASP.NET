using Pcf.GivingToCustomer.Core.Domain.Enums;
using System.Collections.Generic;

namespace Pcf.GivingToCustomer.WebHost.Models
{
	public class CreateOrEditCustomerRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public List<Preference> Preferences { get; set; }
	}
}