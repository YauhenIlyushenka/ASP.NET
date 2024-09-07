using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoCodeFactory.WebHost.Infrastructure;
using PromoCodeFactory.WebHost.Infrastructure.ExceptionHandling;
using PromoCodeFactory.WebHost.Infrastructure.Swagger;
using System.Text.Json.Serialization;

namespace PromoCodeFactory.WebHost
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});

			services.AddFluentValidationAutoValidation();
			services.AddValidators();

			services.AddPromoCodeFactoryServices();
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
		}
	}
}