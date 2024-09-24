using System;

namespace PromoCodeFactory.Core.Helpers
{
	public static class DateTimeHelper
	{
		private const string DateFormat = "MM/dd/yyyy";

		public static string ToDateString(this DateTime date, string format = DateFormat) => date.ToString(format);
	}
}
