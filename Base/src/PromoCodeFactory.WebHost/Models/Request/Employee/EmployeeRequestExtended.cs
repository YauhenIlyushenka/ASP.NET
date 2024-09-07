using PromoCodeFactory.Core.Domain.Administration.Enum;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public class EmployeeRequestExtended : BaseEmployeeRequest
	{
		public Guid Id { get; set; }
		public List<Role> Roles { get; set; }
	}
}
