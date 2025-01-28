using Newtonsoft.Json;
using Pcf.ReceivingFromPartner.Core.Exceptions;
using Pcf.ReceivingFromPartner.Core.Exceptions.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration
{
	public abstract class ErrorHandlingGateway
	{
		public async Task HandlingGatewayErrorResponse(HttpResponseMessage response)
		{
			var errorContent = await response.Content.ReadAsStringAsync();
			var errorModel = JsonConvert.DeserializeObject<ErrorModel>(errorContent);

			switch (response.StatusCode)
			{
				case HttpStatusCode.BadRequest:
					throw new BadRequestException(errorModel.Detail);
				case HttpStatusCode.NotFound:
					throw new NotFoundException(errorModel.Detail);
				case HttpStatusCode.BadGateway:
					throw new BadGatewayException();
				case HttpStatusCode.GatewayTimeout:
					throw new GatewayTimeoutException();
				case HttpStatusCode.ServiceUnavailable:
					throw new ServiceUnavailableException();
				default:
					throw new Exception(errorModel.Detail ?? string.Empty);
			}
		}
	}
}
