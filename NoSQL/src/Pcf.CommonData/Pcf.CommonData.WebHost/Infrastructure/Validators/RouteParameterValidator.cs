using FluentValidation;

namespace Pcf.CommonData.WebHost.Infrastructure.Validators
{
	public class RouteParameterValidator : AbstractValidator<int>
	{
		public RouteParameterValidator()
		{
			RuleFor(x => x)
				.GreaterThan(0)
				.WithMessage("Id should be greater then 0.");
		}
	}
}
