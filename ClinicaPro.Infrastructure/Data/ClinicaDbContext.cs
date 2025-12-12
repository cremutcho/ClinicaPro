using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Infrastructure.Data
{
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

        public DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public DbSet<Cargo> Cargos { get; set; } = null!;

        public DbSet<ContaPagar> ContasPagar { get; set; } = null!;
        public DbSet<ContaReceber> ContasReceber { get; set; } = null!;
        public DbSet<Pagamento> Pagamentos { get; set; } = null!;
        public DbSet<Servico> Servicos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices e relacionamentos
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

            // =========================================================
            // 🔧 Correções MÍNIMAS dos warnings dos campos decimal
            // =========================================================

            modelBuilder.Entity<Cargo>()
                .Property(c => c.Salario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ContaPagar>()
                .Property(c => c.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ContaReceber>()
                .Property(c => c.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Servico>()
                .Property(s => s.ValorPadrao)
                .HasPrecision(18, 2);
        }
    }
}
