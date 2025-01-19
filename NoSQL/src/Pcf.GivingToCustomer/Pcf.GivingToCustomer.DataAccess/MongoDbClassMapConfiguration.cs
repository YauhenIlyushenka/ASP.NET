using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pcf.GivingToCustomer.Core.Domain;
using System;

namespace Pcf.GivingToCustomer.DataAccess
{
	public static class MongoDbClassMapConfiguration
	{
		public static void Configure()
		{
			// Global setup for Guid
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
			//BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

			BsonClassMap.RegisterClassMap<Preference>(cm =>
			{
				cm.AutoMap(); // Auto Mapping all fields
				cm.SetIdMember(cm.GetMemberMap(c => c.Id)); // Set unique identifier
				cm.SetIgnoreExtraElements(true); // Ignore any extra fields between entity in code and db
			});

			BsonClassMap.RegisterClassMap<PromoCode>(cm =>
			{
				cm.AutoMap();
				cm.SetIdMember(cm.GetMemberMap(c => c.Id));
				cm.SetIgnoreExtraElements(true);
			});

			BsonClassMap.RegisterClassMap<Customer>(cm =>
			{
				cm.AutoMap();
				cm.SetIdMember(cm.GetMemberMap(c => c.Id));
				cm.SetIgnoreExtraElements(true);
			});
		}
	}
}
