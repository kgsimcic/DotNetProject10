using Microsoft.AspNetCore.Mvc;
using PatientMicroservice.Entities;
using PatientMicroservice.Models;
using PatientMicroservice.Services;

namespace PatientMicroservice.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PatientController(IPatientService patientService, ILogger<PatientController> logger, IReportService reportService) : ControllerBase
    {
        private readonly ILogger<PatientController> _logger = logger;
        private readonly IPatientService _patientService = patientService;
        private readonly IReportService _reportService = reportService;

        [HttpGet("Patients")]
        public async Task<ActionResult> GetPatients()
        {
            return Ok(await _patientService.GetPatients());
        }

        [HttpGet("Patients/{patientId}")]
        public async Task<IActionResult> GetById(int patientId)
        {
            return Ok(await _patientService.GetPatientById(patientId));
        }

        [HttpPost("Patients")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientModel patientModel)
        {
            int patientId = await _patientService.CreatePatient(patientModel);
            return Ok(patientId);
        }

        [HttpPut("Patients/{Id}")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientModel patientModel, int Id)
        {
            if (patientModel.Id != Id)
            {
                return BadRequest("Id of request does not match patient info passed.");
            } else
            {
                await _patientService.UpdatePatient(patientModel);
                return Created();
            }
        }

        [HttpGet("Patients/Report/{patientId}")]
        public async Task<ActionResult> GenerateReport(int patientId)
        {
            string result = await _reportService.GenerateReport(patientId);
            Console.WriteLine(result);
            return Ok(result);
        }
    }
}
