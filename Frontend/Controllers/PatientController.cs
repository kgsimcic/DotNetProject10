using Frontend.Models;
using Frontend.Services;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Frontend.Controllers
{
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _patientService;

        public PatientController(ILogger<PatientController> logger, IPatientService patientService)
        {
            _logger = logger;
            _patientService = patientService;
        }

        public async Task<IActionResult> GetAll()
        {
            IEnumerable<PatientModel?> patients = await _patientService.GetPatients();
            return View(patients);
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        public async Task<IActionResult> Create([FromForm] PatientViewModel patientViewModel)
        {
            await _patientService.Create(patientViewModel);
            return RedirectToAction(nameof(GetAll));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
