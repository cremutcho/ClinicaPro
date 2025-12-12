using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Servicos.Commands
{
    public class CriarServicoCommand : IRequest<Servico>
    {
        public string Nome { get; set; } = string.Empty; // inicializado para evitar warning
        public string? CodigoTuss { get; set; } 
        public decimal ValorPadrao { get; set; }
    }
}
