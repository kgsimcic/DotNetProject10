using PatientMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using PatientMicroservice.Models;

namespace PatientMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NoteController> _logger;

        public NoteController(INoteService noteService, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            _logger = logger;
        }

        [HttpGet("Notes/{patientId}")]
        public async Task<ActionResult> GetNotes(int patientId)
        {
            return Ok(await _noteService.GetNotes(patientId.ToString()));
        }

        [HttpPost("Notes")]
        public async Task<IActionResult> CreateNote([FromBody] DoctorNoteModel noteModel)
        {
            await _noteService.Create(noteModel);
            return Created();
        }
    }
}
