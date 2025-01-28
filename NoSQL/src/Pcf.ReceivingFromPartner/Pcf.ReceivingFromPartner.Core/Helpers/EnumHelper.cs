using System;
using System.ComponentModel;
using System.Reflection;

namespace Pcf.ReceivingFromPartner.Core.Helpers
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
			var attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute), false);

			return attribute != null ? attribute.Description : string.Empty;
		}
	}
}
