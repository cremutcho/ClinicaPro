using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Especialidades.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

namespace ClinicaPro.Core.Features.Especialidades.Handlers
{
    public class CriarEspecialidadeCommandHandler : IRequestHandler<CriarEspecialidadeCommand, Especialidade>
    {
        private readonly IEspecialidadeRepository _repository;

        public CriarEspecialidadeCommandHandler(IEspecialidadeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Especialidade> Handle(CriarEspecialidadeCommand request, CancellationToken cancellationToken)
        {
            var especialidade = new Especialidade
            {
                Nome = request.Nome
            };

            // Retorna o ID inserido pelo repositório
            especialidade.Id = await _repository.AddAsync(especialidade);

            return especialidade;
        }
    }
}
