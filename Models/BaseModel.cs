using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace AmaselBE.Model {

    public class BaseModel {
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonProperty ("id")]
        public string Code { get; set; }

        public string PlatformUrl { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTime.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTime.Now;

        public ObjectState State { get; set; } = ObjectState.New;

        public string GetTableName () {
            return this.GetType ().Name;
        }

    }
}