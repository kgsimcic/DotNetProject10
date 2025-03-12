using System.ComponentModel.DataAnnotations;

namespace PatientMicroservice.Entities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public DateTime Dob {  get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}
