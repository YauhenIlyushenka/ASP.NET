using System.ComponentModel;

namespace PromoCodeFactory.Core.Domain.Enums
{
	public enum Preference
	{
		None = 0,

		[Description("Theater")]
		Theater = 1,

		[Description("Family")]
		Family = 2,

		[Description("Children")]
		Children = 3,
	}
}
