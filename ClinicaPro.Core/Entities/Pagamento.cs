using ClinicaPro.Core.Enums; // certifique-se deste using

namespace ClinicaPro.Core.Entities
{
    public class Pagamento
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Descricao { get; set; }

        // AQUI: apenas uma propriedade, do tipo enum
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPagamento Status { get; set; } = StatusPagamento.Pendente;

        // 🔹 Novo: referência ao paciente
       
    }
}
