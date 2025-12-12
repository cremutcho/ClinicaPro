using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Handlers
{
    public class ExcluirEspecialidadeCommandHandler : IRequestHandler<ExcluirEspecialidadeCommand, bool>
    {
        private readonly IEspecialidadeRepository _repository;

        public ExcluirEspecialidadeCommandHandler(IEspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ExcluirEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.ExistsAsync(request.Id);

            if (!exists)
                return false;

            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}
