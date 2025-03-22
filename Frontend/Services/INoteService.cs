using Frontend.Models;

namespace Frontend.Services
{
    public interface INoteService
    {
        Task<IEnumerable<DoctorNoteModel?>> GetNotes(int patientId);
        Task<bool> Create(DoctorNoteModel noteViewModel);
    }
}
