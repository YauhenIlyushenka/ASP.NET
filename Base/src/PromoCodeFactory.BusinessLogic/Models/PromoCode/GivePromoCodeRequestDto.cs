using EnumPreference = PromoCodeFactory.Core.Domain.Enums.Preference;

namespace PromoCodeFactory.BusinessLogic.Models.PromoCode
{
	public class GivePromoCodeRequestDto
	{
		public string ServiceInfo { get; init; }
		public string PartnerName { get; init; }
		public string PromoCode { get; init; }
		public string BeginDate { get; init; }
		public string EndDate { get; init; }
		public Guid EmployeeId { get; init; }
		public EnumPreference Preference { get; init; }
	}
}
