using System.ComponentModel;

namespace Pcf.ReceivingFromPartner.Core.Domain.Enum
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
