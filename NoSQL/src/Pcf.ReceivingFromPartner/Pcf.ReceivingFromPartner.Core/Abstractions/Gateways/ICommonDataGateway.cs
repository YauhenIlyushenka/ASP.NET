using Pcf.ReceivingFromPartner.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Core.Abstractions.Gateways
{
	public interface ICommonDataGateway
	{
		Task<List<Preference>> GetAllPreferencesAsync();
		Task<Preference> GetPreferenceByIdAsync(int id);
	}
}
