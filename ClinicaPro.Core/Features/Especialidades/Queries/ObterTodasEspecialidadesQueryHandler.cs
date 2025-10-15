// ClinicaPro.Core/Features/Especialidades/Queries/ObterTodasEspecialidadesQueryHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Especialidades.Queries
{
    public class ObterTodasEspecialidadesQueryHandler : IRequestHandler<ObterTodasEspecialidadesQuery, IEnumerable<Especialidade>>
    {
        // Certifique-se de que IEspecialidadeRepository está acessível.
        private readonly IEspecialidadeRepository _especialidadeRepository;

        public ObterTodasEspecialidadesQueryHandler(IEspecialidadeRepository especialidadeRepository)
        {
            _especialidadeRepository = especialidadeRepository;
        }

        public async Task<IEnumerable<Especialidade>> Handle(ObterTodasEspecialidadesQuery request, CancellationToken cancellationToken)
        {
            return await _especialidadeRepository.GetAllAsync();
        }
    }
}