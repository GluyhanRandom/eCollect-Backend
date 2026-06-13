using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace eCollectAPI.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public DateTime? dateCreated { get; set; }
    }
}
