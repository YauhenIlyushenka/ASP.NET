using FluentValidation;
using PromoCodeFactory.Core.Domain.Enums;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.Customer;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class CustomerValidator : BaseCommonValidator<CreateOrEditCustomerRequest>
	{
		public CustomerValidator()
		{
			RuleFor(x => x.Preferences)
				.Must(ValidateRolesFields)
				.WithMessage($"Invalid Preference has been detected. You should choose any preferences from the pull: {string.Join(", ", EnumHelper.ToList<Preference>().Where(preference => preference != Preference.None))}");
		}

		private bool ValidateRolesFields(List<Preference> enteredPreferences)
		{
			foreach (var preference in enteredPreferences)
			{
				if (!EnumHelper.ToList<Preference>().Where(preference => preference != Preference.None).Contains(preference))
				{
					return false;
				}
			}

			return true;
		}
	}
}
