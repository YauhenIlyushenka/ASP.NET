using FluentValidation;
using Pcf.ReceivingFromPartner.Core.Domain.Enum;
using Pcf.ReceivingFromPartner.WebHost.Models.Request;
using System;
using System.Linq;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure.Validators
{
	public class ReceivingPromoCodeValidator : AbstractValidator<ReceivingPromoCodeRequest>
	{
		public ReceivingPromoCodeValidator()
		{
			RuleFor(x => x.PromoCode).NotEmpty().MaximumLength(16);
			RuleFor(x => x.ServiceInfo).NotEmpty().MaximumLength(32);
			RuleFor(x => x.Preference)
				.Must(ValidatePreferenceField)
				.WithMessage($"Preference must not be set up in {Preference.None}. You should choose any preference from the pull: {string.Join(", ", Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList().Where(preference => preference != Preference.None))}");
		}

		private bool ValidatePreferenceField(Preference enteredPreference)
			=> Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList()
			.Where(preference => preference != Preference.None)
			.Any(preference => preference == enteredPreference);
	}
}
