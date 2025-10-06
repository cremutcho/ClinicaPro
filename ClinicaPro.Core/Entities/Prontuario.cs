namespace ClinicaPro.Core.Entities
{
    public class Prontuario
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        
        public string Descricao { get; set; } = null!;
        public List<string>? Arquivos { get; set; }  // Lista de paths ou URLs para exames/receitas
    }
}
