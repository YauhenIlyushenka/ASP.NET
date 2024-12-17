using System;

namespace Pcf.Administration.Core.Exceptions
{
	[Serializable]
	public class BadRequestException : Exception
	{
		public BadRequestException()
		{
		}

		public BadRequestException(string message)
			: base(message)
		{
		}

		public BadRequestException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
