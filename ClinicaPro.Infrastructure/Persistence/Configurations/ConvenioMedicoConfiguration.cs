using ClinicaPro.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicaPro.Infrastructure.Persistence.Configurations
{
    public class ConvenioMedicoConfiguration 
        : IEntityTypeConfiguration<ConvenioMedico>
    {
        public void Configure(EntityTypeBuilder<ConvenioMedico> builder)
        {
            builder.ToTable("ConveniosMedicos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.Ativo)
                   .IsRequired();

            builder.Property(c => c.DataCadastro)
                   .IsRequired();
        }
    }
}
