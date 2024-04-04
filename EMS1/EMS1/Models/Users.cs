using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EMS1.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
       public string? Id { get; set; }
        public string? name { get; set; }
       public string? Email { get; set; }
        public string? Password { get; set; }

        public string? role { get; set; }
    }
}
