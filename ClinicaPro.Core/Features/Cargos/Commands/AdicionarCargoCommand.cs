using MediatR;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    // O comando implementa IRequest<T>, onde T é o tipo de retorno esperado (neste caso, o Id do novo Cargo).
    public record AdicionarCargoCommand : IRequest<int>
    {
        public string Nome { get; init; } = string.Empty;
        public string Descricao { get; init; } = string.Empty;
    }
}