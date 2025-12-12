using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Queries
{
    public class ObterEspecialidadePorIdQueryHandler : IRequestHandler<ObterEspecialidadePorIdQuery, Especialidade?>
    {
        private readonly IEspecialidadeRepository _repository;

        public ObterEspecialidadePorIdQueryHandler(IEspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Especialidade?> Handle(ObterEspecialidadePorIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
