namespace Pcf.CommonData.WebHost.Infrastructure.Swagger
{
	public static class SwaggerConfigurator
	{
		public static void AddSwaggerServices(this IServiceCollection services)
		{
			services.AddOpenApiDocument(options =>
			{
				options.Title = "Dictionary Data API Doc";
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
