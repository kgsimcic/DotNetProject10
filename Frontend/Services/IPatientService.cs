using Frontend.Models;
using Frontend.ViewModels;

namespace Frontend.Services
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientModel?>> GetPatients();
        public Task<bool> Create(PatientViewModel patientViewModel);
    }
}
