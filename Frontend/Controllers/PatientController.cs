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
            IEnumerable<PatientModel?> patients = await _patientService.GetPatients();
            return View(patients);
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        public async Task<IActionResult> UpdateForm(int patientId)
        {
            PatientModel patient = await _patientService.GetById(patientId);
            return View(patient); 
        }

        public async Task<IActionResult> Create([FromForm] PatientViewModel patientViewModel)
        {
            await _patientService.Create(patientViewModel);
            return RedirectToAction(nameof(GetAll));
        }

        public async Task<IActionResult> Update([FromForm] PatientModel patientModel)
        {
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
