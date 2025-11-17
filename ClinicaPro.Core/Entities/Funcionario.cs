namespace ClinicaPro.Core.Entities
{
    public class Funcionario 
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public DateTime DataContratacao { get; set; }
        public string Cargo { get; set; } = null!; 
        public bool Ativo { get; set; }
    }
}