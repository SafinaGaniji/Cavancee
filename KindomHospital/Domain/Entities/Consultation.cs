namespace KindomHospital.Domain.Entities
{
    /** Réprésente une consultation médicale entre un docteur et un patient.
     */
    public class Consultation
    {
        [Key]
        public int Id { get; set; }

        // --- Relations ---
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        // --- Données de la consultation ---
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [MaxLength(100)]
        public string? Reason { get; set; }

        // --- Navigation ---
        public ICollection<Ordonnance> Ordonnances { get; set; } = new List<Ordonnance>();
    }
}
