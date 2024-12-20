namespace Pcf.GivingToCustomer.Core.Domain
{
	public interface IEntity<TId>
	{
		TId Id { get; set; }
	}
}
