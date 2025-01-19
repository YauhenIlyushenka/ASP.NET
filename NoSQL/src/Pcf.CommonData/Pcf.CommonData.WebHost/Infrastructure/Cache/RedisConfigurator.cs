namespace Pcf.CommonData.WebHost.Infrastructure.Cache
{
	public static class RedisConfigurator
	{
		public static void ConfigureRedisCache(this IServiceCollection services, string connectionString)
		{
			services.AddDistributedMemoryCache();
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = connectionString;
			});
		}
	}
}
