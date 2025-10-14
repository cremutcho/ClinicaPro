using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Infrastructure.Data
{
    // Agora herda de IdentityDbContext para suportar autenticação e roles
    public class ClinicaDbContext : IdentityDbContext<IdentityUser>
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Medico> Medicos { get; set; } = null!;
        public DbSet<Especialidade> Especialidades { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;
        public DbSet<Prontuario> Prontuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Chaves e relacionamentos
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            modelBuilder.Entity<Medico>()
                .HasIndex(m => m.CRM)
                .IsUnique();

            modelBuilder.Entity<Prontuario>()
                .HasOne(p => p.Paciente)
                .WithOne(pac => pac.Prontuario)
                .HasForeignKey<Prontuario>(p => p.PacienteId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId);
        }
    }
}
