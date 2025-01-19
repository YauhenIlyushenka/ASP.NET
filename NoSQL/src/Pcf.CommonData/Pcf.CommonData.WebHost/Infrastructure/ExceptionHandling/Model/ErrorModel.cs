using Newtonsoft.Json;

namespace Pcf.CommonData.WebHost.Infrastructure.ExceptionHandling.Model
{
	public class ErrorModel
	{
		public int Status { get; set; }
		public required string Title { get; set; }
		public required string Detail { get; set; }
		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}
