using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pcf.Administration.Core.Settings;
using Pcf.Administration.DataAccess;
using Pcf.Administration.WebHost.Infrastructure;
using Pcf.Administration.WebHost.Infrastructure.ExceptionHandling;
using Pcf.Administration.WebHost.Infrastructure.RabbitMQ;
using Pcf.Administration.WebHost.Infrastructure.RabbitMQ.Model;
using Pcf.Administration.WebHost.Infrastructure.Swagger;
using Pcf.Administration.WebHost.Infrastructure.Validators;
using System.Text.Json.Serialization;

namespace Pcf.Administration.WebHost
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

			services.ConfigureMongoDb(Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>());
			services.ConfigureMassTransit(Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>());
			services.AddAdministrationServices();
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
			
			app.SeedDatabase<DataContext>();
		}
	}
}