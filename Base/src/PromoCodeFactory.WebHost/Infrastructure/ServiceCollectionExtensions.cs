using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.WebHost.Infrastructure.Validators;
using PromoCodeFactory.WebHost.Models.Request;

namespace PromoCodeFactory.WebHost.Infrastructure
{
	public static class ServiceCollectionExtensions
	{
		public static void AddValidators(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<BaseCommonValidator<BaseCommonRequest>>();
			services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
			services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
		}

		public static void AddSwaggerServices(this IServiceCollection services)
		{
			services.AddOpenApiDocument(options =>
			{
				options.Title = "PromoCode Factory API Doc";
				options.Version = "1.0";
			});
		}

		public static void UseSwaggerServices(this IApplicationBuilder app)
		{
			app.UseOpenApi();
			app.UseSwaggerUi(x =>
			{
				x.DocExpansion = "list";
			});
		}
	}
}
