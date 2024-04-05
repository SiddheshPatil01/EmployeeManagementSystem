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
       public string? email { get; set; }
        public string? password { get; set; }

        public string? role { get; set; }
        public int? mngId { get; set; }
        public int? empId { get; set; }
    }
}
