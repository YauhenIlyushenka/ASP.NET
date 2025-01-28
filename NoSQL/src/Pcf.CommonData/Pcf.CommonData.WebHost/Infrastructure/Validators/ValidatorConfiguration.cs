using FluentValidation;

namespace Pcf.CommonData.WebHost.Infrastructure.Validators
{
	public static class ValidatorConfiguration
	{
		public static void AddValidators(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<RouteParameterValidator>();
		}
	}
}
