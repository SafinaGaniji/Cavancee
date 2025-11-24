namespace KindomHospital.Domain.Entities
{
    public class Medicament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string DosageFrom { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Strength { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? ActCode { get; set; }

        public ICollection<OrdonnanceLigne> OrdonnanceLignes { get; set; } = new List<OrdonnanceLigne>();
    }
}
