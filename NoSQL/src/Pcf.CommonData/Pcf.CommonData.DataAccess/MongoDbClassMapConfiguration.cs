using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pcf.CommonData.Core.Domain;

namespace Pcf.CommonData.DataAccess
{
	public static class MongoDbClassMapConfiguration
	{
		public static void Configure()
		{
			// Global setup for Guid
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));

			BsonClassMap.RegisterClassMap<Preference>(cm =>
			{
				cm.AutoMap(); // Auto Mapping all fields
				cm.SetIdMember(cm.GetMemberMap(c => c.Id)); // Set unique identifier
				cm.SetIgnoreExtraElements(true); // Ignore any extra fields between entity in code and db
			});
		}
	}
}
