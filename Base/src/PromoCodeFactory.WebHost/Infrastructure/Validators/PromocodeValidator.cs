using FluentValidation;
using PromoCodeFactory.Core.Domain.Enums;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.PromoCode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class PromocodeValidator : AbstractValidator<GivePromoCodeRequest>
	{
		public PromocodeValidator()
		{
			RuleFor(x => x.PromoCode).NotEmpty().MaximumLength(16);
			RuleFor(x => x.PartnerName).NotEmpty().MaximumLength(16);
			RuleFor(x => x.ServiceInfo).NotEmpty().MaximumLength(32);
			RuleFor(x => x.EmployeeId).NotEmpty();
			RuleFor(x => new { x.BeginDate, x.EndDate })
				.Must(x => ValidateDateFields(x.BeginDate, x.EndDate))
				.WithMessage($"Invalid BeginDate or EndDate. You should enter date in format \'{DateTimeHelper.DateFormat}\'");
			RuleFor(x => x.Preference)
				.Must(ValidatePreferenceField)
				.WithMessage($"Preference must not be set up in {Preference.None}. You should choose any preference from the pull: {string.Join(", ", EnumHelper.ToList<Preference>().Where(preference => preference != Preference.None))}");
		}

		private bool ValidatePreferenceField(Preference enteredPreference)
			=> EnumHelper
			.ToList<Preference>()
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
