namespace Frontend.Services
{
    public interface IReportService
    {
        public Task<string> GenerateReport(int patientId);
    }
}
