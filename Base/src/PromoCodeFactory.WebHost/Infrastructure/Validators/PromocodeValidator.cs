using FluentValidation;
using PromoCodeFactory.Core.Domain.Enums;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.PromoCode;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class PromocodeValidator : AbstractValidator<GivePromoCodeRequest>
	{
		public PromocodeValidator()
		{
			RuleFor(x => x.PromoCode).NotEmpty();
			RuleFor(x => x.PartnerName).NotEmpty();
			RuleFor(x => x.ServiceInfo).NotEmpty();
			RuleFor(x => x.Preference)
				.Must(ValidatePreferenceField)
				.WithMessage($"Preference must not be set up in {Preference.None}. You should choose any preference from the pull: {string.Join(", ", EnumHelper.ToList<Preference>().Where(preference => preference != Preference.None))}");
		}

		private bool ValidatePreferenceField(Preference enteredPreference)
			=> EnumHelper
			.ToList<Preference>()
			.Where(preference => preference != Preference.None)
			.Any(preference => preference == enteredPreference);
		}
}
