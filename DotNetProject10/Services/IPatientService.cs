using PatientMicroservice.Models;

namespace PatientMicroservice.Services
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientModel?>> GetPatients();
        public Task<int> UpdatePatient(PatientModel patientModel);
        public Task<int> CreatePatient(PatientModel patientModel);
    }
}
