using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Queries;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Handlers
{
    public class ObterTodasEspecialidadesQueryEspecialidadeByIdQueryHandler : IRequestHandler<ObterTodasEspecialidadesQueryEspecialidadeByIdQuery, Especialidade?>
    {
        private readonly IEspecialidadeRepository _repository;

        public ObterTodasEspecialidadesQueryEspecialidadeByIdQueryHandler(IEspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Especialidade?> Handle(ObterTodasEspecialidadesQueryEspecialidadeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
