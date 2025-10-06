namespace ClinicaPro.Core.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Email { get; set; } = null!;
        
        public List<Consulta> Consultas { get; set; } = new();
        public Prontuario? Prontuario { get; set; }
    }
}
