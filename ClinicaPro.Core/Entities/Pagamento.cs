using ClinicaPro.Core.Enums; // certifique-se deste using
using System;

namespace ClinicaPro.Core.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Descricao { get; set; } = string.Empty; // inicialização para evitar warning

        // Propriedades do tipo enum
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento Status { get; set; } = StatusPagamento.Pendente;

        // 🔹 Referência ao paciente (caso queira adicionar futuramente)
        // public int PacienteId { get; set; }
        // public Paciente? Paciente { get; set; }
    }
}
