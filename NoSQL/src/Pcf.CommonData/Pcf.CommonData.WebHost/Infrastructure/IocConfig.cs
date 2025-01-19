using Pcf.CommonData.Business.Services;
using Pcf.CommonData.Business.Services.Implementation;
using Pcf.CommonData.Core.Core.Abstractions;
using Pcf.CommonData.Core.Domain;
using Pcf.CommonData.DataAccess.Data;
using Pcf.CommonData.DataAccess.Repository;

namespace Pcf.CommonData.WebHost.Infrastructure
{
	public static class IocConfig
	{
		public static void AddCommonServices(this IServiceCollection services)
		{
			AddCommonBusinessLogicServices(services);
			AddDataAccessLayerRepositories(services);
		}

		private static void AddCommonBusinessLogicServices(this IServiceCollection services)
			=> services.AddScoped<IPreferenceService, PreferenceService>();

		private static void AddDataAccessLayerRepositories(this IServiceCollection services)
		{
			services.AddScoped<IMongoDbInitializer, MongoDbInitializer>();

			services.AddScoped<IMongoRepository<Preference, int>, MongoRepository<Preference, int>>();
			services.AddScoped<IMongoRepository<Preference, int>, CachingPreferenceRepository>();
		}
	}
}
