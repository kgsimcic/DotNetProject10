using Microsoft.AspNetCore.Mvc;
using PatientMicroservice.Models;
using PatientMicroservice.Services;

namespace PatientMicroservice.Controllers
{
    public class PatientFilterCriteria
    {
        public DateTime Dob {  get; set; }
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet("Patients")]
        public async Task<ActionResult> GetPatients()
        {
            return Ok(await _patientService.GetPatients());
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
                // need to add actual code here.
                return Created();
            }
        }
    }
}
