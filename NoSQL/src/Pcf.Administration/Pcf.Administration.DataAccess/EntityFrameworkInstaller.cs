using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pcf.Administration.Core.Settings;

namespace Pcf.Administration.DataAccess
{
	public static class EntityFrameworkInstaller
	{
		public static void ConfigureMongoDb(this IServiceCollection services, MongoDbSettings settings)
		{
			MongoDbClassMapConfiguration.Configure();

			services.AddDbContext<DataContext>(optionsBuilder =>
				optionsBuilder.UseMongoDB(settings.ConnectionString, settings.DatabaseName));
		}
	}
}
