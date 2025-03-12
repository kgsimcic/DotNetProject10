using MongoDB.Driver;
using PatientMicroservice.Entities;

namespace PatientMicroservice.Services
{
    public class NoteService : INoteService
    {
        private readonly IMongoCollection<DoctorNote> _notesCollection;

        public NoteService(IMongoDatabase database)
        {
            _notesCollection = database.GetCollection<DoctorNote>("DoctorNotes");
        }

        // Add CRUD methods here
        public async Task<List<DoctorNote>> GetAllAsync() =>
            await _notesCollection.Find(_ => true).ToListAsync();

        public async Task<DoctorNote> GetByIdAsync(string id) =>
            await _notesCollection.Find(note => note.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DoctorNote note) =>
            await _notesCollection.InsertOneAsync(note);

    }
}
