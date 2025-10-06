using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClinicaPro.Infrastructure.Data
{
    public class ClinicaDbContextFactory : IDesignTimeDbContextFactory<ClinicaDbContext>
{
    public ClinicaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ClinicaDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-HP0L5QF\\MSSQLSERVER01;Database=ClinicaProDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new ClinicaDbContext(optionsBuilder.Options);
    }
}
}
