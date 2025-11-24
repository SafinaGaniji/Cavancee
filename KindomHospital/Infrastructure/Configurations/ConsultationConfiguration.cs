using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindomHospital.Domain.Entities;

namespace KindomHospital.Infrastructure.Configurations
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            // Nom de table (optionnel mais propre)
            builder.ToTable("Consultations");

            // PRIMARY KEY
            builder.HasKey(c => c.Id);

            // Propriétés simples
            builder.Property(c => c.Reason)
                   .HasMaxLength(100);

            builder.Property(c => c.Date)
                   .IsRequired();

            builder.Property(c => c.Time)
                   .IsRequired();

            // RELATION : Consultation → Doctor (Many-to-One)
            builder.HasOne(c => c.Doctor)
                   .WithMany(d => d.Consultations)
                   .HasForeignKey(c => c.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // RELATION : Consultation → Patient (Many-to-One)
            builder.HasOne(c => c.Patient)
                   .WithMany(p => p.Consultations)
                   .HasForeignKey(c => c.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // RELATION : Consultation → Ordonnances (One-to-Many)
            builder.HasMany(c => c.Ordonnances)
                   .WithOne(o => o.Consultation)
                   .HasForeignKey(o => o.ConsultationId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
