using Newtonsoft.Json;

namespace Frontend.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;
        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://webapi:7185");
        }

        public async Task<string> GenerateReport(int patientId)
        {
            var response = await _httpClient.GetAsync($"/Patients/Report/{patientId}");
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadAsStringAsync());
        }
    }
}
