using MediatR;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    // O retorno pode ser void (Unit), indicando apenas sucesso ou falha na operação.
    public record AtualizarCargoCommand : IRequest<Unit>
    {
        public int Id { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Descricao { get; init; } = string.Empty;
    }
}