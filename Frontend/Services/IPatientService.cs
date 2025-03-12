using Frontend.Models;

namespace Frontend.Services
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientModel?>> GetPatients();
    }
}
