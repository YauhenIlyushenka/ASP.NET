using Newtonsoft.Json;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure.ExceptionHandling.Modell
{
	public class ErrorModel
	{
		[JsonProperty("status")]
		public required int Status { get; set; }

		[JsonProperty("title")]
		public required string Title { get; set; }

		[JsonProperty("detail")]
		public required string Detail { get; set; }
		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}
