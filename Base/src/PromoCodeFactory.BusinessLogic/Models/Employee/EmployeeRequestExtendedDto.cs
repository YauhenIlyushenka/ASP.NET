using PromoCodeFactory.BusinessLogic.Models.Role;

namespace PromoCodeFactory.BusinessLogic.Models.Employee
{
	public class EmployeeRequestExtendedDto
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int AppliedPromocodesCount { get; set; }

		public List<Guid> RoleIds { get; set; }
	}
}
