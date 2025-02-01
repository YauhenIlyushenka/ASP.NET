using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pcf.GivingToCustomer.Core.Settings;
using Pcf.GivingToCustomer.DataAccess;
using Pcf.GivingToCustomer.DataAccess.Data;
using Pcf.GivingToCustomer.WebHost.Infrastructure;
using Pcf.GivingToCustomer.WebHost.Infrastructure.ExceptionHandling;
using Pcf.GivingToCustomer.WebHost.Infrastructure.RabbitMQ.Model;
using Pcf.GivingToCustomer.WebHost.Infrastructure.RabbitMQ;
using Pcf.GivingToCustomer.WebHost.Infrastructure.Swagger;
using Pcf.GivingToCustomer.WebHost.Infrastructure.Validators;
using System.Text.Json.Serialization;

namespace Pcf.GivingToCustomer.WebHost
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
			services.AddGivingToCustomerServices();
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