using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClinicaPro.Infrastructure.Data
{
    public class ClinicaDbContextFactory : IDesignTimeDbContextFactory<ClinicaDbContext>
    {
        public ClinicaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicaDbContext>();

            // 🔥 AGORA USANDO SQLITE
            optionsBuilder.UseSqlite("Data Source=clinicapro.db");

            return new ClinicaDbContext(optionsBuilder.Options);
        }
    }
}