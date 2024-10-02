using PromoCodeFactory.BusinessLogic.Models.Preference;
using PromoCodeFactory.BusinessLogic.Models.PromoCode;

namespace PromoCodeFactory.BusinessLogic.Models.Customer
{
	public class CustomerResponseDto : CustomerResponseBaseModel
	{
		public List<PromoCodeShortResponseDto> PromoCodes { get; set; } = [];
		public List<PreferenceResponseDto> Preferences { get; set; } = [];
	}
}