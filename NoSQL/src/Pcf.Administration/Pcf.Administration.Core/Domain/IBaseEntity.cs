using System;

namespace Pcf.Administration.Core.Domain
{
	public interface IBaseEntity<TId>
	{
		public TId Id { get; set; }
	}
}