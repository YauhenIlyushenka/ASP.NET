namespace PromoCodeFactory.WebHost.Models.Request
{
	public abstract class BaseCommonRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}
}
