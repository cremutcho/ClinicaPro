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
        public Paciente? Paciente { get; set; }

        public int MedicoId { get; set; }
        public Medico? Medico { get; set; }

        public int? ServicoId { get; set; }
        public Servico? Servico { get; set; }

        // 🔽 AQUI nasce o sentido do Convênio (do jeito certo)
        public Guid? ConvenioMedicoId { get; set; }
        public ConvenioMedico? ConvenioMedico { get; set; }

        public string? Observacoes { get; set; }
    }
}
