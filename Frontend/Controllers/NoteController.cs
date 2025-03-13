using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult GetAll(int patientId)
        {
            return View();
        }
    }
}
