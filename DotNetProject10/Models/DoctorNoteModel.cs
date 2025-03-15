using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PatientMicroservice.Models
{
    public class DoctorNoteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int PatientId { get; set; } = 0!;

        public string Note { get; set; } = null!;
    }
}
