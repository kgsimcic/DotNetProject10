using Frontend.Models;
using Frontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _noteService;

        public NoteController(ILogger<NoteController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        public async Task<IActionResult> Create([FromForm] DoctorNoteModel noteModel)
        {
            await _noteService.Create(noteModel);
            return RedirectToAction(nameof(GetNotes), new { patientId = noteModel.PatientId });
        }

        public IActionResult CreateForm(int patientId, string patientName)
        {
            ViewBag.PatientName = patientName;
            return View(
                new DoctorNoteModel {
                    PatientId = patientId
                });
        }

        public async Task<IActionResult> GetNotes(int patientId, string patientName)
        {
            ViewBag.PatientName = patientName;
            ViewBag.PatientId = patientId;
            IEnumerable<DoctorNoteModel?> notes = await _noteService.GetNotes(patientId);
            return View(notes);
        }
    }
}
