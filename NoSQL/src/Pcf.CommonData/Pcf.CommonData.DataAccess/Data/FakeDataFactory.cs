using Pcf.CommonData.Core.Domain;
using EnumPreference = Pcf.CommonData.Core.Enum.Preference;

namespace Pcf.CommonData.DataAccess.Data
{
	public static class FakeDataFactory
	{
		public static List<Preference> Preferences => new List<Preference>()
		{
			new Preference()
			{
				Id = (int)EnumPreference.Theater,
				Name = EnumPreference.Theater.ToString(),
			},
			new Preference()
			{
				Id = (int)EnumPreference.Family,
				Name = EnumPreference.Family.ToString(),
			},
			new Preference()
			{
				Id = (int)EnumPreference.Children,
				Name = EnumPreference.Children.ToString(),
			}
		};
	}
}
