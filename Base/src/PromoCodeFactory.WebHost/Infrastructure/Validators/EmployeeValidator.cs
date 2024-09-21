using FluentValidation;
using PromoCodeFactory.Core.Domain.Administration.Enum;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.Employee;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class EmployeeValidator: AbstractValidator<EmployeeRequest>
	{
		public EmployeeValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(32);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Role)
				.Must(ValidateRoleField)
				.WithMessage($"Role must not be set up in {Role.None}. You should choose any role from the pull: {string.Join(", ", EnumHelper.ToList<Role>().Where(role => role != Role.None))}");
		}

		private bool ValidateRoleField(Role enteredRole)
			=> EnumHelper.ToList<Role>().Where(role => role != Role.None).Any(role => role == enteredRole);
	}
}
