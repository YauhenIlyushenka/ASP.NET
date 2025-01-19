using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Core.Abstractions.Repositories
{
	public interface IPreferenceRepository : IMongoRepository<Preference, int>
	{
		Task<List<Preference>> GetPreferencesByIds(List<int> ids);
		Task UpdateCustomerIdsInPreferences(
			Guid customerId,
			List<Preference> updatePreferences,
			List<int> oldPreferenceIds = null);

		Task UpdatePreferencesByRemovingCustomer(
			List<Preference> preferences,
			Guid customerId,
			List<Guid> promocodeIdsByCustomer);

		Task UpdatePreferenceWithNewPromocodes(int preferenceId, List<Guid> promocodeIds);
	}
}
