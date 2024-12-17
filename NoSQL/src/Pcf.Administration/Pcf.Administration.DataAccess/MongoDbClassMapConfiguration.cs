using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pcf.Administration.Core.Domain.Administration;

namespace Pcf.Administration.DataAccess
{
	public static class MongoDbClassMapConfiguration
	{
		public static void Configure()
		{
			// Global setup for Guid
			BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

			BsonClassMap.RegisterClassMap<Employee>(cm =>
			{
				cm.AutoMap(); // Auto Mapping all fields
				cm.SetIdMember(cm.GetMemberMap(c => c.Id)); // Set unique identifier
				cm.SetIgnoreExtraElements(true); // Ignore any extra fields between entity in code and db
			});

			BsonClassMap.RegisterClassMap<GlobalRole>(cm =>
			{
				cm.AutoMap();
				cm.SetIdMember(cm.GetMemberMap(c => c.Id));
				cm.SetIgnoreExtraElements(true);
			});
		}
	}
}
