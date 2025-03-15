using MongoDB.Driver;
using PatientMicroservice.Entities;
using PatientMicroservice.Models;

namespace PatientMicroservice.Services
{
    public class NoteService(IMongoDatabase database, ILogger<NoteService> logger) : INoteService
    {
        private readonly IMongoCollection<DoctorNote> _notesCollection = database.GetCollection<DoctorNote>("DoctorNotes");
        private readonly ILogger<NoteService> _logger = logger;

        public async Task<IEnumerable<DoctorNoteModel>> GetNotes(string patientId)
        {
            IEnumerable<DoctorNote> notes = await _notesCollection.Find(note => note.PatientId == patientId).ToListAsync();
            return notes.Select(n => new DoctorNoteModel
            {
                PatientId = Convert.ToInt32(n.PatientId),
                Note = n.Note
            });
        }


        public async Task<int> Create(DoctorNoteModel noteModel)
        {
            var note = new DoctorNote
            {
                PatientId = noteModel.PatientId.ToString(),
                Note = noteModel.Note
            };

            try
            {
                await _notesCollection.InsertOneAsync(note);
                return 1;
            }
            catch (MongoWriteException ex)
            {
                _logger.LogError(ex, "Failed to insert note: {Message}", ex.Message);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error inserting note: {Message}", ex.Message);
                return 0;
            }
        }

    }
}
