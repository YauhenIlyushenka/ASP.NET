using FluentValidation;
using Pcf.GivingToCustomer.Core.Domain.Enums;
using Pcf.GivingToCustomer.Core.Helpers;
using Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure.Validators
{
	public class PromocodeValidator : AbstractValidator<GivePromoCodeRequest>
	{
		public PromocodeValidator()
		{
			RuleFor(x => x.PromoCode).NotEmpty().MaximumLength(16);
			RuleFor(x => x.ServiceInfo).NotEmpty().MaximumLength(32);
			RuleFor(x => x.PartnerId).NotEmpty();
			RuleFor(x => new { x.BeginDate, x.EndDate })
				.Must(x => ValidateDateFields(x.BeginDate, x.EndDate))
				.WithMessage($"Invalid BeginDate or EndDate. You should enter date in format \'{DateTimeHelper.DateFormat}\'");
			RuleFor(x => x.Preference)
				.Must(ValidatePreferenceField)
				.WithMessage($"Preference must not be set up in {Preference.None}. You should choose any preference from the pull: {string.Join(", ", Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList().Where(preference => preference != Preference.None))}");
		}

		private bool ValidatePreferenceField(Preference enteredPreference)
			=> Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList()
				.Where(preference => preference != Preference.None)
				.Any(preference => preference == enteredPreference);

		private bool ValidateDateFields(string enteredBeginDate, string enteredEndDate)
		{
			if (!IsValidFormatDates([enteredBeginDate, enteredEndDate]))
			{
				return false;
			}

			var beginDate = enteredBeginDate.ToDateTime();
			var endDate = enteredEndDate.ToDateTime();
			var currentDate = DateTime.Now;

			return beginDate > currentDate && endDate > currentDate && endDate > beginDate;
		}

		private bool IsValidFormatDates(List<string> dates)
			=> dates.All(date => DateTime.TryParseExact(date, DateTimeHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _));
	}
}
