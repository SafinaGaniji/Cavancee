using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KindomHospital.Infrastructure
{
    // Fournit un DbContext au moment de conception pour dotnet-ef
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            // Chaîne de connexion locale (LocalDB). Ajustez si vous utilisez un autre serveur.
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=KindomHospitalDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}

