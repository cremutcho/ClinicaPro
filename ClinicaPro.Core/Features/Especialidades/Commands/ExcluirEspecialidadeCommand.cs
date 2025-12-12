using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Commands
{
    public class ExcluirEspecialidadeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
