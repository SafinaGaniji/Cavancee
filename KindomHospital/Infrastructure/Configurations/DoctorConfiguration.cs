using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.FirstName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(d => d.LastName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasOne(d => d.Specialty)
                   .WithMany(s => s.Doctors)
                   .HasForeignKey(d => d.SpecialtyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Index pour recherche rapide
            builder.HasIndex(d => new { d.LastName, d.FirstName });
        }
    }
}
