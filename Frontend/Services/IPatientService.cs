using Frontend.Models;
using Frontend.ViewModels;

namespace Frontend.Services
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientModel?>> GetPatients();
        public Task<PatientModel> GetById(int patientId);
        public Task<bool> Create(PatientViewModel patientViewModel);
        public Task<bool> Update(PatientModel patientModel);
    }
}
