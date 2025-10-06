namespace ClinicaPro.Core.Entities
{
    public class Especialidade
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        
        public List<Medico> Medicos { get; set; } = new();
    }
}

