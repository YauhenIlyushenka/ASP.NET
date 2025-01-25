﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Pcf.ReceivingFromPartner.Core.Exceptions;
using Pcf.ReceivingFromPartner.Core.Exceptions.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.WebHost.Infrastructure.ExceptionHandling
{
	public static class ExceptionHandler
	{
		private const string ResponseContentType = "application/json";

		public static void UseErrorHandler(this IApplicationBuilder application)
		{
			application.UseExceptionHandler(applicationError =>
			{
				applicationError.Run(async context =>
				{
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						await HandleExceptionAsync(context.Response, contextFeature.Error);
					}
				});
			});
		}

		private static async Task HandleExceptionAsync(HttpResponse response, Exception? exception)
		{
			if (exception == null)
			{
				return;
			}

			response.ContentType = ResponseContentType;
			response.StatusCode = exception switch
			{
				ArgumentException or BadRequestException => (int)HttpStatusCode.BadRequest,
				NotFoundException => (int)HttpStatusCode.NotFound,
				BadGatewayException => (int)HttpStatusCode.BadGateway,
				GatewayTimeoutException => (int)HttpStatusCode.GatewayTimeout,
				ServiceUnavailableException => (int)HttpStatusCode.ServiceUnavailable,
				_ => (int)HttpStatusCode.InternalServerError,
			};

			await response.WriteAsync(new ErrorModel
			{
				Status = response.StatusCode,
				Title = ((HttpStatusCode)response.StatusCode).ToString(),
				Detail = exception.InnerException?.Message ?? exception.Message,
			}.ToString());
		}
	}
}
