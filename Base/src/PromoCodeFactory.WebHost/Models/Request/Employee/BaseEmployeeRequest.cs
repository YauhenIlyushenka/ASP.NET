namespace PromoCodeFactory.WebHost.Models.Request.Employee
{
	public abstract class BaseEmployeeRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int AppliedPromocodesCount { get; set; }
	}
}
