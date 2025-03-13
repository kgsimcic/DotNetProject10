using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModels
{
    public class PatientViewModel
    {
        [Required]
        public string GivenName { get; set; } = null!;
        [Required]
        public string FamilyName { get; set; } = null!;
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Dob { get; set; }
        [Required]
        public string Sex { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
    }
}
