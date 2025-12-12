using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Handlers
{
    public class AtualizarEspecialidadeCommandHandler : IRequestHandler<AtualizarEspecialidadeCommand, Especialidade?>
    {
        private readonly IEspecialidadeRepository _repository;

        public AtualizarEspecialidadeCommandHandler(IEspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Especialidade?> Handle(AtualizarEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            var especialidade = await _repository.GetByIdAsync(request.Id);

            if (especialidade is null)
                return null;

            especialidade.Nome = request.Nome;

            await _repository.UpdateAsync(especialidade);

            return especialidade;
        }
    }
}
