namespace Pcf.ReceivingFromPartner.Core.Domain
{
	public interface IEntity<TId>
	{
		TId Id { get; set; }
	}
}
