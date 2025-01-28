using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pcf.ReceivingFromPartner.DataAccess;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.ExceptionHandling;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.Settings;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.Swagger;
using Pcf.ReceivingFromPartner.WebHost.Infrastructure.Validators;
using System.Text.Json.Serialization;

namespace Pcf.ReceivingFromPartner.WebHost
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

			services.AddFluentValidationAutoValidation();
			services.AddValidators();

			services.ConfigureDbContext(Configuration.Get<ApplicationSettings>().ConnectionString);
			services.AddReceivingFromPartnerServices(Configuration);

			services.AddSwaggerServices();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

			app.MigrateDatabase<DataContext>();
		}
	}
}