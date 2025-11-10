namespace ClinicaPro.Core.Entities
{
    // A classe Funcionario AGORA NÃO HERDA de nenhuma BaseEntity, 
    // espelhando a estrutura do Paciente.
    public class Funcionario 
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public DateTime DataContratacao { get; set; }
        public string Cargo { get; set; } = null!; // Ex: Recepcionista, Gerente, etc.
        public bool Ativo { get; set; }
    }
}