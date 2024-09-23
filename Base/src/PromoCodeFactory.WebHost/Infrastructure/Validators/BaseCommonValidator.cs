using FluentValidation;
using PromoCodeFactory.WebHost.Models.Request;

namespace PromoCodeFactory.WebHost.Infrastructure.Validators
{
	public class BaseCommonValidator<T> : AbstractValidator<T> where T : BaseCommonRequest
	{
		public BaseCommonValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().MaximumLength(32);
			RuleFor(x => x.LastName).NotEmpty().MaximumLength(64);
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
		}
	}
}
