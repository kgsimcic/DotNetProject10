using Frontend.Models;
using Frontend.Services;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Frontend.Controllers
{
    public class PatientController(ILogger<PatientController> logger, IPatientService patientService, IReportService reportService) : Controller
    {
        private readonly ILogger<PatientController> _logger = logger;
        private readonly IPatientService _patientService = patientService;
        private readonly IReportService _reportService = reportService;

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PatientModel>? patients = await _patientService.GetPatients();
            return View(new PatientViewModel() { Patients = patients });
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        public async Task<IActionResult> UpdateForm(int patientId)
        {
            PatientModel patient = await _patientService.GetById(patientId);
            PatientCreateFormViewModel patientCreateFormViewModel = new()
            {
                Id = patient.Id,
                GivenName = patient.GivenName,
                FamilyName = patient.FamilyName,
                Dob = DateOnly.FromDateTime(patient.Dob),
                Sex = patient.Sex,
                Address = patient.Address,
                Phone = patient.Phone
            };
            return View(patientCreateFormViewModel); 
        }

        public async Task<IActionResult> Create([FromForm] PatientCreateFormViewModel patientCreateFormViewModel)
        {
            PatientModel patientModel = new()
            {
                GivenName = patientCreateFormViewModel.GivenName,
                FamilyName = patientCreateFormViewModel.FamilyName,
                Dob = patientCreateFormViewModel.Dob.ToDateTime(TimeOnly.Parse("00:00 AM")),
                Sex = patientCreateFormViewModel.Sex,
                Address = patientCreateFormViewModel.Address,
                Phone = patientCreateFormViewModel.Phone
            };
            await _patientService.Create(patientModel);
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Update([FromForm] PatientCreateFormViewModel patientViewModel)
        {
            PatientModel patientModel = new()
            {
                GivenName = patientViewModel.GivenName,
                FamilyName = patientViewModel.FamilyName,
                Dob = patientViewModel.Dob.ToDateTime(TimeOnly.Parse("00:00 AM")),
                Sex = patientViewModel.Sex,
                Address = patientViewModel.Address,
                Phone = patientViewModel.Phone
            };

            await _patientService.Update(patientModel);
            return RedirectToAction(nameof(GetAll));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GenerateReport(int patientId)
        {
            string result = await _reportService.GenerateReport(patientId);
            return View(new ResultViewModel { Result = result });
        }
    }
}
