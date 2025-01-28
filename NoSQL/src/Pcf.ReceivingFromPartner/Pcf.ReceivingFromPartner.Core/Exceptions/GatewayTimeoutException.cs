using System;

namespace Pcf.ReceivingFromPartner.Core.Exceptions
{
	[Serializable]
	public class GatewayTimeoutException : Exception
	{
		public GatewayTimeoutException()
		{
		}

		public GatewayTimeoutException(string message)
			: base(message)
		{
		}

		public GatewayTimeoutException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
