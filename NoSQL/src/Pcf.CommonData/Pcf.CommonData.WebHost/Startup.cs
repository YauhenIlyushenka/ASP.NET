using FluentValidation.AspNetCore;
using Pcf.CommonData.Core.Settings;
using Pcf.CommonData.DataAccess;
using Pcf.CommonData.DataAccess.Data;
using Pcf.CommonData.WebHost.Infrastructure;
using Pcf.CommonData.WebHost.Infrastructure.Cache;
using Pcf.CommonData.WebHost.Infrastructure.ExceptionHandling;
using Pcf.CommonData.WebHost.Infrastructure.Swagger;
using Pcf.CommonData.WebHost.Infrastructure.Validators;

namespace Pcf.CommonData.WebHost
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddFluentValidationAutoValidation();
			services.AddValidators();

			services.ConfigureRedisCache(Configuration.Get<RedisSettings>().ConnectionString);
			services.ConfigureMongoDb(Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>());

			services.AddCommonServices();
			services.AddSwaggerServices();
		}

		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env,
			IMongoDbInitializer mongoDbInitializer)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseSwaggerServices();

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseErrorHandler();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			mongoDbInitializer.Seed();
		}
	}
}
