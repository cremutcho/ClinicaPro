using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public record UpdateMedicoCommand : IRequest<Unit>
    {
        public required Medico Medico { get; init; }
    }
}
