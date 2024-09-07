﻿using Newtonsoft.Json;

namespace PromoCodeFactory.WebHost.Infrastructure.ExceptionHandling.Model
{
	public class ErrorModel
	{
		public int Status { get; set; }

		public string Title { get; set; }

		public string Detail { get; set; }

		public override string ToString() => JsonConvert.SerializeObject(this);
	}
}
