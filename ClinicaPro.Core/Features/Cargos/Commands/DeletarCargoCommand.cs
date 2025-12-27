using MediatR;

namespace ClinicaPro.Core.Features.Cargos.Commands
{
    public record DeletarCargoCommand : IRequest<Unit>
    {
        public int Id { get; init; }
    }
}