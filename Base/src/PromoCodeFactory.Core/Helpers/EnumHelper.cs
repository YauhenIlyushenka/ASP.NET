using System.ComponentModel;
using System;

namespace PromoCodeFactory.Core.Helpers
{
	public static class EnumHelper
	{
		public static string GetDescription(Enum value)
		{
			if (value == null)
			{
				return string.Empty;
			}

			var data = value.ToString();

			var field = value.GetType().GetField(data);
			var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

			return attributes.Length > 0 ? attributes[0].Description : string.Empty;
		}
	}
}
