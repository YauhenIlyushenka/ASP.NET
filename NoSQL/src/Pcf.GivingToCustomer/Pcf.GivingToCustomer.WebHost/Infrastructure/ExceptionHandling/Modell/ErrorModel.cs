using Newtonsoft.Json;

namespace Pcf.GivingToCustomer.WebHost.Infrastructure.ExceptionHandling.Modell
{
	public class ErrorModel
	{
		public int Status { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}
