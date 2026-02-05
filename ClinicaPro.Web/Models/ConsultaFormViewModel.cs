using System;
using System.ComponentModel.DataAnnotations;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Web.Models
{
    public class ConsultaFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public StatusConsulta Status { get; set; }

        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int MedicoId { get; set; }

        public int? ServicoId { get; set; }

        public string? Observacoes { get; set; }

        // 🧠 decisão de negócio
        [Required]
        public string TipoAtendimento { get; set; } = "Particular";

        // só quando for convênio
        public Guid? ConvenioMedicoId { get; set; }
    }
}
