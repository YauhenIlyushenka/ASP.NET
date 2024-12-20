using FluentValidation;
using Pcf.Administration.Core.Domain.Enums;
using Pcf.Administration.WebHost.Models.Request;
using System;
using System.Linq;

namespace Pcf.Administration.WebHost.Infrastructure.Validators
{
	public class EmployeeValidator : AbstractValidator<EmployeeRequest>
	{
		public EmployeeValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(32);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.AppliedPromocodesCount).GreaterThan(0);
			RuleFor(x => x.Role).Must(ValidateTokenRegionType);
		}

		private bool ValidateTokenRegionType(Role role)
		{
			var IsValidRegionType = Role.None != role;

			if (!IsValidRegionType)
			{
				throw new ArgumentException(string.Format(
					"{0} should not be set up in {1}. Region Types are valid: {2}",
					nameof(Role),
					Role.None,
					string.Join(", ", Enum.GetValues(typeof(Role)).Cast<Role>().ToList().Where(x => x != Role.None))));
			}

			return IsValidRegionType;
		}
	}
}
