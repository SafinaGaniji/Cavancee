using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class OrdonnanceLigneConfiguration : IEntityTypeConfiguration<OrdonnanceLigne>
    {
        public void Configure(EntityTypeBuilder<OrdonnanceLigne> builder)
        {
            builder.ToTable("OrdonnanceLignes");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Dosage)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(l => l.Frequency)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(l => l.Duration)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(l => l.Quantity)
                .IsRequired();

            builder.Property(l => l.Instructions)
                .HasMaxLength(255)
                .IsRequired(false);

            // Relations
            builder.HasOne(l => l.Ordonnance)
                .WithMany(o => o.OrdonnanceLignes)
                .HasForeignKey(l => l.OrdonnanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Medicament)
                .WithMany(m => m.OrdonnanceLignes)
                .HasForeignKey(l => l.MedicamentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}