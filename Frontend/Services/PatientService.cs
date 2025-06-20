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
            _httpClient.BaseAddress = new Uri("http://webapi:7185");
        }

        public async Task<IEnumerable<PatientModel>?> GetPatients()
        {
            var response = await _httpClient.GetAsync("/Patients");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            string responseJson = responseString.Replace("\\", "").Trim('"');

            IEnumerable<PatientModel>? patients = JsonConvert.DeserializeObject<IEnumerable<PatientModel>?>(responseJson) ?? [];
            return (patients);
        }

        public async Task<PatientModel> GetById(int patientId)
        {
            var response = await _httpClient.GetAsync($"/Patients/{patientId}");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            string responseJson = responseString.Replace("\\", "").Trim('"');

            PatientModel patientModel = JsonConvert.DeserializeObject<PatientModel>(responseJson) ?? new PatientModel();
            return (patientModel);
        }

        public async Task<bool> Create(PatientModel patientModel)
        {

            string requestString = JsonConvert.SerializeObject(patientModel);
            StringContent content = new (requestString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Patients", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(PatientModel patientModel)
        {
            string requestString = JsonConvert.SerializeObject(patientModel);
            StringContent content = new(requestString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/Patients/{patientModel.Id}", content);
            return response.IsSuccessStatusCode;
        }


    }
}
