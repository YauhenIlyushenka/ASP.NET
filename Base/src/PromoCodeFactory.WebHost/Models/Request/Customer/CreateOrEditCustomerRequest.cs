using PromoCodeFactory.Core.Domain.Enums;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models.Request.Customer
{
	public class CreateOrEditCustomerRequest : BaseCommonRequest
	{
		public List<Preference> Preferences { get; set; }
	}
}
