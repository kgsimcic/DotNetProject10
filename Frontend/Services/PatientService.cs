using Frontend.Models;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;
        public PatientService(HttpClient httpClient) {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7185");
        }

        public async Task<IEnumerable<PatientModel?>> GetPatients()
        {
            var response = await _httpClient.GetAsync("/Patient/Patients");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            string responseJson = responseString.Replace("\\", "").Trim(new[] { '"' });

            IEnumerable<Models.PatientModel?> patients = JsonConvert.DeserializeObject<IEnumerable<PatientModel?>>(responseJson);
            return (patients);
        }

        public async Task<bool> Create(PatientViewModel patientViewModel)
        {
            PatientModel patientModel = new PatientModel {
                GivenName = patientViewModel.GivenName,
                FamilyName = patientViewModel.FamilyName,
                Dob = patientViewModel.Dob.ToDateTime(TimeOnly.Parse("00:00 AM")),
                Sex = patientViewModel.Sex,
                Address = patientViewModel.Address,
                Phone = patientViewModel.Phone
            };

            string requestString = JsonConvert.SerializeObject(patientModel);
            StringContent content = new (requestString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Patient/Patients", content);
            return response.IsSuccessStatusCode;
        }
    }
}
