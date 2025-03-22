using Frontend.Models;

namespace Frontend.ViewModels
{
    public class DoctorNoteViewModel
    {
        public int PatientId { get; set; } = 0!;
        public string? PatientName { get; set; }
        public IEnumerable<DoctorNoteModel>? Notes { get; set; }
    }
}
