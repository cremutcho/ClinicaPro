using MediatR;

namespace ClinicaPro.Core.Features.Cargos.Queries
{
    // Retorna um CargoDto, que é o DTO de listagem/exibição
    public record BuscarCargoPorIdQuery : IRequest<CargoDto?>
    {
        public int Id { get; init; }
    }
}