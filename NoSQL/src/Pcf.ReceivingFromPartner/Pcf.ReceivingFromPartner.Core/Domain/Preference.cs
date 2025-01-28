using Newtonsoft.Json;

namespace Pcf.ReceivingFromPartner.Core.Domain
{
	public class Preference : IEntity<int>
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}