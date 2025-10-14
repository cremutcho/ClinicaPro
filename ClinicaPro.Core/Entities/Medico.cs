namespace ClinicaPro.Core.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CRM { get; set; } = null!;
        public int EspecialidadeId { get; set; }
        public Especialidade? Especialidade { get; set; } 
        public string Email { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        
        public List<Consulta> Consultas { get; set; } = new();
    }
}
