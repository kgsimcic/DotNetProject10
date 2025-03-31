using Microsoft.AspNetCore.Mvc;
using PatientMicroservice.Entities;
using PatientMicroservice.Models;
using PatientMicroservice.Services;

namespace PatientMicroservice.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PatientsController(IPatientService patientService, ILogger<PatientsController> logger, IReportService reportService) : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger = logger;
        private readonly IPatientService _patientService = patientService;
        private readonly IReportService _reportService = reportService;

        [HttpGet()]
        public async Task<ActionResult> GetPatients()
        {
            return Ok(await _patientService.GetPatients());
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetById(int patientId)
        {
            return Ok(await _patientService.GetPatientById(patientId));
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePatient([FromBody] PatientModel patientModel)
        {
            int patientId = await _patientService.CreatePatient(patientModel);
            return Ok(patientId);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientModel patientModel, int Id)
        {
            if (patientModel.Id != Id)
            {
                return BadRequest("Id of request does not match patient info passed.");
            }
            else
            {
                await _patientService.UpdatePatient(patientModel);
                return Created();
            }
        }

        [HttpGet("Report/{patientId}")]
        public async Task<ActionResult> GenerateReport(int patientId)
        {
            string result = await _reportService.GenerateReport(patientId);
            Console.WriteLine(result);
            return Ok(result);
        }
    }
}
