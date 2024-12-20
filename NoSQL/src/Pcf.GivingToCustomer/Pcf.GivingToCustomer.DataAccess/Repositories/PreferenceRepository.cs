using MongoDB.Driver;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.DataAccess.Repositories
{
	public class PreferenceRepository : MongoRepository<Preference, int>, IPreferenceRepository
	{
		public PreferenceRepository(IMongoDatabase database) : base(database)
		{ }

		public async Task<List<Preference>> GetPreferencesByIds(List<int> ids)
			=> await Collection
				.Aggregate()
				.Match(c => ids.Contains(c.Id))
				.ToListAsync();

		public async Task UpdateCustomerIdsInPreferences(
			Guid customerId,
			List<Preference> updatePreferences,
			List<int> oldPrefereneIds = null)
		{
			var bulkUpdatesPreference = new List<WriteModel<Preference>>();

			foreach (var preference in updatePreferences)
			{
				if (!preference.CustomerIds.Contains(customerId))
				{
					var updatePreference = Builders<Preference>.Update.Push(p => p.CustomerIds, customerId);
					bulkUpdatesPreference.Add(new UpdateOneModel<Preference>(
						Builders<Preference>.Filter.Eq(p => p.Id, preference.Id),
						updatePreference
					));
				}
			}

			if (oldPrefereneIds != null)
			{
				foreach (var oldPreferenceId in oldPrefereneIds)
				{
					if (!updatePreferences.Select(x => x.Id).Contains(oldPreferenceId))
					{
						var updateOldPreference = Builders<Preference>.Update.Pull(p => p.CustomerIds, customerId);
						bulkUpdatesPreference.Add(new UpdateOneModel<Preference>(
							Builders<Preference>.Filter.Eq(p => p.Id, oldPreferenceId),
							updateOldPreference
						));
					}
				}
			}

			if (bulkUpdatesPreference.Any())
			{
				await Collection.BulkWriteAsync(bulkUpdatesPreference);
			}
		}

		public async Task UpdatePreferencesByRemovingCustomer(
			List<Preference> preferences,
			Guid customerId,
			List<Guid> promocodeIdsByCustomer)
		{
			var bulkUpdateForPreferences = new List<WriteModel<Preference>>();

			foreach (var preference in preferences)
			{
				var updateCustomerIds = Builders<Preference>.Update.Pull(p => p.CustomerIds, customerId);
				bulkUpdateForPreferences.Add(new UpdateOneModel<Preference>(
					Builders<Preference>.Filter.Eq(p => p.Id, preference.Id),
					updateCustomerIds
				));

				var updatePromoCodeIds = Builders<Preference>.Update.PullFilter(p => p.PromoCodeIds, promoCodeId => promocodeIdsByCustomer.Contains(promoCodeId));
				bulkUpdateForPreferences.Add(new UpdateOneModel<Preference>(
					Builders<Preference>.Filter.Eq(p => p.Id, preference.Id),
					updatePromoCodeIds
				));
			}

			if (bulkUpdateForPreferences.Any())
			{
				await Collection.BulkWriteAsync(bulkUpdateForPreferences);
			}
		}

		public async Task UpdatePreferenceWithNewPromocodes(int preferenceId, List<Guid> promocodeIds)
		{
			var bulkUpdateForPreference = new List<WriteModel<Preference>>();

			foreach (var promocodeId in promocodeIds)
			{
				var updatePreference = Builders<Preference>.Update.Push(p => p.PromoCodeIds, promocodeId);
				bulkUpdateForPreference.Add(new UpdateOneModel<Preference>(
					Builders<Preference>.Filter.Eq(p => p.Id, preferenceId),
					updatePreference
				));
			}

			if (bulkUpdateForPreference.Any())
			{
				await Collection.BulkWriteAsync(bulkUpdateForPreference);
			}
		}
	}
}
