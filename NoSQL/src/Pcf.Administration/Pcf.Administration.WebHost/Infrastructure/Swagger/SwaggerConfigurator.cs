using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Pcf.Administration.WebHost.Infrastructure.Swagger
{
	public static class SwaggerConfigurator
	{
		public static void AddSwaggerServices(this IServiceCollection services)
		{
			services.AddOpenApiDocument(options =>
			{
				options.Title = "PromoCode Factory Administration API Doc";
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
