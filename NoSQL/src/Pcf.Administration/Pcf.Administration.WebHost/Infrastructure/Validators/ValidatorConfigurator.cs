using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Pcf.Administration.WebHost.Infrastructure.Validators
{
	public static class ValidatorConfigurator
	{
		public static void AddValidators(this IServiceCollection services)
			=> services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
	}
}
