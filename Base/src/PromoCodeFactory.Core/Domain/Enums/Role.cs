﻿using System.ComponentModel;

namespace PromoCodeFactory.Core.Domain.Enums
{
	public enum Role
	{
		None = 0,

		[Description("Administrator")]
		Admin = 1,

		[Description("Partner Manager")]
		PartnerManager = 2
	}
}
