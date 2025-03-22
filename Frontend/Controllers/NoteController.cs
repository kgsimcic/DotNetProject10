using Frontend.Models;
using Frontend.Services;
using Frontend.ViewModels;
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

        public async Task<IActionResult> Create([FromForm] NoteCreateFormViewModel noteCreateFormViewModel)
        {
            DoctorNoteModel noteModel = new ()
            {
                PatientId = noteCreateFormViewModel.PatientId,
                Note = noteCreateFormViewModel.Note
            };
            await _noteService.Create(noteModel);
            return RedirectToAction(nameof(GetNotes), new { patientId = noteModel.PatientId });
        }

        public IActionResult CreateForm(int patientId, string patientName)
        {
            return View(
                new DoctorNoteViewModel {
                    PatientId = patientId,
                    PatientName = patientName
                });
        }

        public async Task<IActionResult> GetNotes(int patientId, string patientName)
        {
            IEnumerable<DoctorNoteModel> notes = await _noteService.GetNotes(patientId);
            return View(new DoctorNoteViewModel
            {
                PatientId = patientId,
                PatientName = patientName,
                Notes = notes
            });
        }
    }
}
