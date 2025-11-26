using ClinicaPro.Core.Entities.Enums;
using MediatR;
using System;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Commands
{
    public class UpdateContaPagarCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }

        public StatusFinanceiro Status { get; set; }   // 🔥 Agora está correto!

        public string Categoria { get; set; } = string.Empty;
    }
}
