namespace Pcf.ReceivingFromPartner.Core.Domain
{
	public class Preference : IEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}