using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromoCodeFactory.DataAccess;
using PromoCodeFactory.DataAccess.Data;
using PromoCodeFactory.WebHost.Infrastructure.Validators;
using PromoCodeFactory.WebHost.Models.Request;
using System;

namespace PromoCodeFactory.WebHost.Infrastructure
{
	public static class ServiceCollectionExtensions
	{
		public static void AddValidators(this IServiceCollection services)
		{
			services.AddValidatorsFromAssemblyContaining<BaseCommonValidator<BaseCommonRequest>>();
			services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
			services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
			services.AddValidatorsFromAssemblyContaining<PromocodeValidator>();
			services.AddValidatorsFromAssemblyContaining<PartnerPromocodeLimitValidator>();
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

		public static void MigrateDatabase<T>(this IApplicationBuilder application) where T : DatabaseContext
		{
			var scope = application.ApplicationServices.CreateScope();
			var dbContext = scope.ServiceProvider.GetService<T>();

			//dbContext.Database.EnsureDeleted();
			dbContext.Database.Migrate();
			//Seed(scope.ServiceProvider);
		}

		private static void Seed(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();

			//dbContext.AddRange(FakeDataFactory.Roles);
			//dbContext.AddRange(FakeDataFactory.Employees);
			//dbContext.AddRange(FakeDataFactory.Customers);
			dbContext.AddRange(FakeDataFactory.Partners);

			dbContext.SaveChanges();
		}
	}
}
