using PatientMicroservice.Models;

namespace PatientMicroservice.Services
{
    public interface IReportService
    {
        Task<string> GenerateReport(int patientId);
    }
}
