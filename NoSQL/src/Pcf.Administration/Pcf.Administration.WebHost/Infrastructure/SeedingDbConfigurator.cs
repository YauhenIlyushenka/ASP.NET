using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pcf.Administration.DataAccess;
using Pcf.Administration.DataAccess.Data;
using System;

namespace Pcf.Administration.WebHost.Infrastructure
{
	public static class SeedingDbConfigurator
	{
		public static void SeedDatabase<T>(this IApplicationBuilder application) where T : DataContext
		{
			var scope = application.ApplicationServices.CreateScope();
			var dbContext = scope.ServiceProvider.GetService<T>();

			dbContext.Database.EnsureDeleted();
			Seed(scope.ServiceProvider);
		}

		private static void Seed(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = scope.ServiceProvider.GetService<DataContext>();

			dbContext.AddRange(FakeDataFactory.Roles);
			dbContext.AddRange(FakeDataFactory.Employees);

			dbContext.SaveChanges();
		}
	}
}
