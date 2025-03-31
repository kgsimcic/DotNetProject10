using PatientMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using PatientMicroservice.Models;

namespace PatientMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NotesController> _logger;

        public NotesController(INoteService noteService, ILogger<NotesController> logger)
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
