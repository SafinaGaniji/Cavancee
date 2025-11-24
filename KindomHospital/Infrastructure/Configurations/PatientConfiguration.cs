using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(p => p.DateOfBirth)
                   .IsRequired();

            // Index pour recherche rapide
            builder.HasIndex(p => new { p.LastName, p.FirstName, p.DateOfBirth });

            // Relations
            builder.HasMany(p => p.Consultations)
                   .WithOne(c => c.Patient)
                   .HasForeignKey(c => c.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Ordonnances)
                   .WithOne(o => o.Patient)
                   .HasForeignKey(o => o.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
