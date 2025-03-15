using static Azure.Core.HttpHeader;
using PatientMicroservice.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace PatientMicroservice.Services
{
    public class ReportService(ILogger<ReportService> logger, IPatientService patientService, INoteService noteService) : IReportService
    {
        private readonly ILogger<ReportService> _logger = logger;
        private readonly IPatientService _patientService = patientService;
        private readonly INoteService _noteService = noteService;
        private readonly List<string> triggerWords = [
            "Hemoglobin A1C",
            "Microalbumin",
            "Body Height",
            "Body Weight",
            "Smoker",
            "Abnormal",
            "Cholesterol",
            "Dizziness",
            "Relapse",
            "Reaction",
            "Antibodies"
        ];

        private int CountTriggers(IEnumerable<DoctorNoteModel> notes)
        {
            List<string> noteStrings = notes.Select(n => n.Note).ToList();

            return noteStrings.Select(targetString =>
                triggerWords.Count(word => targetString.Contains(word, StringComparison.OrdinalIgnoreCase))).Sum();
        }

        public async Task<string> GenerateReport(int patientId)
        {
            PatientModel patientModel = await _patientService.GetPatientById(patientId);
            IEnumerable<DoctorNoteModel> notes = await _noteService.GetNotes(patientId.ToString());

            int triggerWords = CountTriggers(notes);
            if (triggerWords < 2) return "None";
            if (triggerWords >= 8) return "Early Onset";

            var age = DateTime.Now - patientModel.Dob;
            double ageYears = age.TotalDays/365;

            if (ageYears > 30)
            {
                return triggerWords switch
                {
                    >= 6 => "In Danger",
                    >= 2 => "Borderline",
                    _ => "None"
                };
            } 
            
            if (patientModel.Sex == "M")
            {
                return triggerWords switch
                {
                    >= 5 => "Early Onset",
                    >= 3 => "In Danger",
                    _ => "None"
                };
            }

            return triggerWords switch
            {
                >= 7 => "Early Onset",
                >= 4 => "In Danger",
                _ => "None"
            };
        }
    }
}
