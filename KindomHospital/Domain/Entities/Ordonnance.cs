namespace KindomHospital.Domain.Entities
{
    public class Ordonnance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int? ConsultationId { get; set; }
        public Consultation? Consultation { get; set; }

        [Required]
        public DateTime DatePrescribed { get; set; }

        [MaxLength(255)]
        public string? Notes { get; set; }

        public ICollection<OrdonnanceLigne> OrdonnanceLignes { get; set; } = new List<OrdonnanceLigne>();
    }
}
