using FluentValidation;
using Pcf.ReceivingFromPartner.Core.Helpers;
using Pcf.ReceivingFromPartner.WebHost.Models.Request;
using System.Globalization;
using System;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure.Validators
{
	public class PartnerPromocodeLimitValidator : AbstractValidator<SetPartnerPromoCodeLimitRequest>
	{
		public PartnerPromocodeLimitValidator()
		{
			RuleFor(x => x.Limit).GreaterThan(0);
			RuleFor(x => x.EndDate)
				.Must(ValidateDateField)
				.WithMessage($"EndDate is invalid. You should enter date in format \'{DateTimeHelper.DateFormat}\'");
		}

		private bool ValidateDateField(string enteredEndDate)
		{
			if (!DateTime.TryParseExact(enteredEndDate, DateTimeHelper.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
			{
				return false;
			}

			var endDate = enteredEndDate.ToDateTime();
			var currentDate = DateTime.Now;

			return endDate > currentDate;
		}
	}
}
