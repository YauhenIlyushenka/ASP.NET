using Newtonsoft.Json;

namespace Pcf.CommonData.Core.Domain
{
	public class Preference : IEntity<int>
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public required string Name { get; set; }
	}
}
