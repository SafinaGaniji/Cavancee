using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class OrdonnanceConfiguration : IEntityTypeConfiguration<Ordonnance>
    {
        public void Configure(EntityTypeBuilder<Ordonnance> builder)
        {
            builder.ToTable("Ordonnances");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.DatePrescribed)
                   .IsRequired();

            builder.Property(o => o.Notes)
                   .HasMaxLength(255)
                   .IsRequired(false);

            // Relations
            builder.HasOne(o => o.Doctor)
                   .WithMany(d => d.Ordonnances)
                   .HasForeignKey(o => o.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Patient)
                   .WithMany(p => p.Ordonnances)
                   .HasForeignKey(o => o.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Consultation)
                   .WithMany(c => c.Ordonnances)
                   .HasForeignKey(o => o.ConsultationId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
