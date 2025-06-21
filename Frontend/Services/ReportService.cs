using Newtonsoft.Json;

namespace Frontend.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BACKEND_URL"));
        }

        public async Task<string> GenerateReport(int patientId)
        {
            var response = await _httpClient.GetAsync($"/Patients/Report/{patientId}");
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadAsStringAsync());
        }
    }
}
