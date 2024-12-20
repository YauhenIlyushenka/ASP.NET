using FluentValidation;
using Pcf.GivingToCustomer.Core.Domain.Enums;
using Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure.Validators
{
	public class CustomerValidator : AbstractValidator<CreateOrEditCustomerRequest>
	{
		public CustomerValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(32);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Preferences)
				.Must(ValidatePreferencesFields)
				.WithMessage($"Invalid Preference has been detected. You should choose any preferences from the pull: {string.Join(", ", Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList().Where(preference => preference != Preference.None))}");
		}

		private bool ValidatePreferencesFields(List<Preference> enteredPreferences)
		{
			foreach (var preference in enteredPreferences)
			{
				if (!Enum.GetValues(typeof(Preference)).Cast<Preference>().ToList().Where(preference => preference != Preference.None).Contains(preference))
				{
					return false;
				}
			}

			return true;
		}
	}
}
