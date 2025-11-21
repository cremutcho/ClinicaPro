namespace ClinicaPro.Core.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }

        // FK
        public int CargoId { get; set; }

        // Navegação
        public Cargo? Cargo { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
