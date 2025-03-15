using PatientMicroservice.Entities;
using PatientMicroservice.Models;

namespace PatientMicroservice.Services
{
    public interface INoteService
    {
        Task<IEnumerable<DoctorNoteModel>> GetNotes(string patientId);
        Task<int> Create(DoctorNoteModel noteModel);
    }
}
