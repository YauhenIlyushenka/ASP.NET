namespace Pcf.Administration.Core.Domain.Administration
{
	public class GlobalRole : IBaseEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}