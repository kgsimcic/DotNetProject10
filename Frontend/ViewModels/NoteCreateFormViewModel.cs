using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class NoteCreateFormViewModel
    {
        public int PatientId { get; set; } = 0!;
        public string? PatientName { get; set; }
        [Required]
        public string Note { get; set; } = null!;
    }
}
