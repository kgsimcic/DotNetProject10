using Frontend.Models;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Services
{
    public class NoteService : INoteService
    {
        private readonly HttpClient _httpClient;
        public NoteService(HttpClient httpClient) {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7185");
        }

        public async Task<IEnumerable<DoctorNoteModel?>> GetNotes(int patientId)
        {
            var response = await _httpClient.GetAsync($"/Note/Notes/{patientId}");
            response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();
            string responseJson = responseString.Replace("\\", "").Trim('"');

            IEnumerable<DoctorNoteModel?> notes = JsonConvert.DeserializeObject<IEnumerable<DoctorNoteModel?>>(responseJson) ?? [];
            return (notes);
        }

        public async Task<bool> Create(DoctorNoteModel noteModel)
        {
            string requestString = JsonConvert.SerializeObject(noteModel);
            StringContent content = new (requestString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/Note/Notes", content);
            return response.IsSuccessStatusCode;
        }
    }
}
