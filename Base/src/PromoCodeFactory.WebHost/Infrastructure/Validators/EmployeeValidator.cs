using FluentValidation;
using PromoCodeFactory.Core.Domain.Enums;
using PromoCodeFactory.Core.Helpers;
using PromoCodeFactory.WebHost.Models.Request.Employee;
using System.Linq;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class EmployeeValidator: BaseCommonValidator<EmployeeRequest>
	{
		public EmployeeValidator()
		{
			RuleFor(x => x.Role)
				.Must(ValidateRoleField)
				.WithMessage($"Role must not be set up in {Role.None}. You should choose any role from the pull: {string.Join(", ", EnumHelper.ToList<Role>().Where(role => role != Role.None))}");
		}

		private bool ValidateRoleField(Role enteredRole)
			=> EnumHelper
			.ToList<Role>()
			.Where(role => role != Role.None)
			.Any(role => role == enteredRole);
	}
}
