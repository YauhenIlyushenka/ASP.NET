namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmpoyeeRequestDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public Guid RoleId { get; set; }
		public int AppliedPromocodesCount { get; set; }
	}
}
