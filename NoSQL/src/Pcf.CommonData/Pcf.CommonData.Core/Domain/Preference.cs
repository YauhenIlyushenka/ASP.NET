namespace Pcf.CommonData.Core.Domain
{
	public class Preference : IEntity<int>
	{
		public int Id { get; set; }
		public required string Name { get; set; }
	}
}
