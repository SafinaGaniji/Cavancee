using KindomHospital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KindomHospital.Infrastructure.Configurations
{
    public class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.ToTable("Medicaments");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(m => m.DosageFrom)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(m => m.Strength)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(m => m.ActCode)
                   .HasMaxLength(20)
                   .IsRequired(false);

            // Relation avec lignes d'ordonnance
            builder.HasMany(m => m.OrdonnanceLignes)
                   .WithOne(l => l.Medicament)
                   .HasForeignKey(l => l.MedicamentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
