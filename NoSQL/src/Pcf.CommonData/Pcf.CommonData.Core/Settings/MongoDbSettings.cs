﻿namespace Pcf.CommonData.Core.Settings
{
	public class MongoDbSettings
	{
		public required string ConnectionString { get; set; }
		public required string DatabaseName { get; set; }
	}
}
