namespace Frontend.Models
{
    public class PatientModel
    {
        public int Id { get; set; }
        public string GivenName { get; set; } = null!;
        public string FamilyName { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Sex { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
