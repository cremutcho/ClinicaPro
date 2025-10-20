using MediatR;

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    public record DeleteConsultaCommand(int Id) : IRequest<Unit>;
}