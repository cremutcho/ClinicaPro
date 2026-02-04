namespace ClinicaPro.Core.Entities
{
    public class ConvenioMedico
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}