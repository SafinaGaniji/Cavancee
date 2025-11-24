using Microsoft.EntityFrameworkCore;
using KindomHospital.Domain.Entities;
using KindomHospital.Infrastructure.Configurations;

namespace KindomHospital.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables de la base de données
        public DbSet<Consultation> Consultations { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Specialty> Specialties { get; set; } = null!;
        public DbSet<Medicament> Medicaments { get; set; } = null!;
        public DbSet<Ordonnance> Ordonnances { get; set; } = null!;
        public DbSet<OrdonnanceLigne> OrdonnanceLignes { get; set; } = null!;




        // Ici plus tard on ajoutera les autres DbSet
        // public DbSet<OrdonnanceLigne> OrdonnanceLignes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConsultationConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new SpecialtyConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
            modelBuilder.ApplyConfiguration(new OrdonnanceConfiguration());
            modelBuilder.ApplyConfiguration(new OrdonnanceLigneConfiguration());
        }
    }
}
