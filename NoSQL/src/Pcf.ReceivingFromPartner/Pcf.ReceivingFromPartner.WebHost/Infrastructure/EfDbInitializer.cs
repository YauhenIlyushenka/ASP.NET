using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pcf.ReceivingFromPartner.DataAccess;
using Pcf.ReceivingFromPartner.DataAccess.Data;
using System;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure
{
	public static class EfDbInitializer
	{
		public static void MigrateDatabase<T>(this IApplicationBuilder application) where T : DataContext
		{
			var scope = application.ApplicationServices.CreateScope();
			var dbContext = scope.ServiceProvider.GetService<T>();

			//dbContext.Database.EnsureDeleted();
			dbContext.Database.Migrate();
			Seed(scope.ServiceProvider);
		}

		private static void Seed(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
			var dbContext = scope.ServiceProvider.GetService<DataContext>();

			dbContext.AddRange(FakeDataFactory.Partners);
			dbContext.SaveChanges();
		}
	}
}
