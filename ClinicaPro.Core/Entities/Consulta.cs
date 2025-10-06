namespace ClinicaPro.Core.Entities
{
    public enum StatusConsulta
    {
        Agendada,
        Concluida,
        Cancelada
    }

    public class Consulta
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public StatusConsulta Status { get; set; } = StatusConsulta.Agendada;
        
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; } = null!;
        
        public int MedicoId { get; set; }
        public Medico Medico { get; set; } = null!;
        
        public string? Observacoes { get; set; }
    }
}

