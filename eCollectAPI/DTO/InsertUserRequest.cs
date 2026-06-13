using MongoDB.Bson.Serialization.Attributes;

namespace eCollectAPI.Model
{
    public class InsertUserRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int age { get; set; }
    }
}