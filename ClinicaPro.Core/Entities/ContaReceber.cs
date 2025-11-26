using System;

namespace ClinicaPro.Core.Entities
{
    public class ContaReceber
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }

        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataRecebimento { get; set; }

        public string Cliente { get; set; } = string.Empty;
    }
}
