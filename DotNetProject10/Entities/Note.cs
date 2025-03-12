using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PatientMicroservice.Entities
{
    public class DoctorNote
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string PatientId { get; set; } = null!;

        public string Note { get; set; } = null!;
    }
}
