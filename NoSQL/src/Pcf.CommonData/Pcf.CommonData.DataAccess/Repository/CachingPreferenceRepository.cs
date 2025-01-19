using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Pcf.CommonData.Core.Core.Abstractions;
using Pcf.CommonData.Core.Domain;
using System.Text.Json;

namespace Pcf.CommonData.DataAccess.Repository
{
	public class CachingPreferenceRepository : IMongoRepository<Preference, int>
	{
		private const string AllPreferencesCacheKey = "AllPreferences";

		private readonly IDistributedCache _distributedCache;
		private readonly ILogger<CachingPreferenceRepository> _logger;
		private readonly IMongoRepository<Preference, int> _preferenceRepository;

		private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(30);

		public CachingPreferenceRepository(
			ILogger<CachingPreferenceRepository> logger,
			IDistributedCache distributedCache,
			IMongoRepository<Preference, int> preferenceRepository)
		{
			_logger = logger;
			_distributedCache = distributedCache;
			_preferenceRepository = preferenceRepository;
		}

		public async Task<List<Preference>> GetAllAsync()
		{	
			// Try to get the item from Redis cache
			var cachedPreferences = await _distributedCache.GetStringAsync(AllPreferencesCacheKey);
			if (cachedPreferences != null)
			{
				_logger.LogInformation("Returning preferences from Redis cache.");
				return JsonSerializer.Deserialize<List<Preference>>(cachedPreferences);
			}
			
			// Db call in order to get preferences
			var preferences = await _preferenceRepository.GetAllAsync();
			_logger.LogInformation("The Preferences have been received from DB.");

			// Cache the preferences in Redis cache
			await _distributedCache.SetStringAsync(
				key: AllPreferencesCacheKey,
				value: JsonSerializer.Serialize(preferences),
				options: new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = _cacheExpiry,
				}
			);

			_logger.LogInformation("The preferences have been added to Redis cache.");
			return preferences;
		}

		public async Task<Preference> GetByIdAsync(int id)
		{
			var cacheKey = $"Preference_{id}";

			var cachedPreferences = await _distributedCache.GetStringAsync(cacheKey);
			if (cachedPreferences != null)
			{
				_logger.LogInformation($"Returning preference with id:{id} from Redis cache.");
				return JsonSerializer.Deserialize<Preference>(cachedPreferences);
			}

			var preference = await _preferenceRepository.GetByIdAsync(id);
			_logger.LogInformation($"The Preference has been received from DB with id:{id}.");

			// Cache the preferences in Redis cache
			await _distributedCache.SetStringAsync(
				key: cacheKey,
				value: JsonSerializer.Serialize(preference),
				options: new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = _cacheExpiry,
				}
			);

			_logger.LogInformation($"The preference with id:{id} has been added to Redis cache.");
			return preference;
		}
	}
}
