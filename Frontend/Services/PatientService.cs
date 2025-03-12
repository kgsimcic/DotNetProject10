using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;
        public PatientService(HttpClient httpClient) {
            _httpClient = httpClient;

            // _httpClient.BaseAddress = new Uri("http://localhost:5001");
            _httpClient.BaseAddress = new Uri("https://localhost:7185");
        }

        public async Task<IEnumerable<PatientModel?>> GetPatients()
        {
            var response = await _httpClient.GetAsync("/Patient/Patients");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            string responseJson = responseString.Replace("\\", "").Trim(new[] { '"' });

            IEnumerable<PatientModel?> patients = JsonConvert.DeserializeObject<IEnumerable<PatientModel?>>(responseJson);
            return (patients);
        }
    }
}
