﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PromoCodeFactory.DataAccess
{
	public static class EntityFrameworkInstaller
	{
		public static void ConfigureDbContext(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<DatabaseContext>(optionsBuilder => optionsBuilder.UseNpgsql(connectionString));
		}
	}
}
