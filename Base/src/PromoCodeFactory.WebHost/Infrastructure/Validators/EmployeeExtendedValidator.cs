using FluentValidation;
using PromoCodeFactory.Core.Domain.Administration.Enum;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.Employee;
using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class EmployeeExtendedValidator : BaseEmployeeValidator<EmployeeRequestExtended>
	{
		public EmployeeExtendedValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
			RuleFor(x => x.Roles)
				.Must(ValidateRolesFields)
				.WithMessage($"Invalid Role has been detected. You should choose any roles from the pull: {string.Join(", ", EnumHelper.ToList<Role>().Where(role => role != Role.None))}");
		}

		private bool ValidateRolesFields(List<Role> enteredRoles)
		{
			foreach (var role in EnumHelper.ToList<Role>().Where(role => role != Role.None))
			{
				if (!enteredRoles.Contains(role))
				{
					return false;
				}
			}

			return true;
		}
	}
}
