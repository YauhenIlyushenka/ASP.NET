﻿using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Settings;

namespace Pcf.GivingToCustomer.DataAccess
{
	public static class MongoDBConfigurator
	{
		public static void ConfigureMongoDb(this IServiceCollection services, MongoDbSettings settings)
		{
			MongoDbClassMapConfiguration.Configure();

			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			services.AddSingleton<IMongoDatabase>(database);
		}
	}
}