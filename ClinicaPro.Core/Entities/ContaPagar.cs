using System;
using ClinicaPro.Core.Entities.Enums;

namespace ClinicaPro.Core.Entities
{
    public class ContaPagar
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }

        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public StatusFinanceiro Status { get; set; }
    }
}
