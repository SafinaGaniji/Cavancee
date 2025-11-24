namespace KindomHospital.Domain.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; } = null!;

        public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
        public ICollection<Ordonnance> Ordonnances { get; set; } = new List<Ordonnance>();
    }
}
