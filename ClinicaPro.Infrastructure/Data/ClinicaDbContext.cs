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

        // ===========================================
        // Entidades Existentes
        // ===========================================
        public DbSet<Paciente> Pacientes { get; set; } = null!;
        public DbSet<Medico> Medicos { get; set; } = null!;
        public DbSet<Especialidade> Especialidades { get; set; } = null!;
        public DbSet<Consulta> Consultas { get; set; } = null!;
        public DbSet<Prontuario> Prontuarios { get; set; } = null!;
        public DbSet<Funcionario> Funcionarios { get; set; } = null!;

        // ===========================================
        // Novas Entidades do Módulo RH 🆕
        // ===========================================
        public DbSet<Cargo> Cargos { get; set; } = null!;
        public DbSet<Departamento> Departamentos { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // É crucial chamar o base.OnModelCreating para configurar as tabelas do Identity
            base.OnModelCreating(modelBuilder); 

            // ===========================================
            // Configurações Existentes (Chaves e Relacionamentos)
            // ===========================================
            
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

            // ===========================================
            // Configurações Adicionais do Módulo RH
            // ===========================================

            // Removidas as configurações de relacionamento com Funcionario (HasOne/WithMany)
            // para evitar erro, pois Funcionario não possui as Foreign Keys (CargoId/DepartamentoId).
            
            // Você pode adicionar regras de unicidade ou índices para as novas entidades aqui
            modelBuilder.Entity<Cargo>()
                .HasIndex(c => c.Nome)
                .IsUnique();

            modelBuilder.Entity<Departamento>()
                .HasIndex(d => d.Nome)
                .IsUnique();
        }
    }
}