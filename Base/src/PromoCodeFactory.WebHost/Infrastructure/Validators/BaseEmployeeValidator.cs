using FluentValidation;
using PromoCodeFactory.WebHost.Models.Request.Employee;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class BaseEmployeeValidator<T> : AbstractValidator<T> where T : BaseEmployeeRequest
	{
		public BaseEmployeeValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(32);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
		}
	}
}
