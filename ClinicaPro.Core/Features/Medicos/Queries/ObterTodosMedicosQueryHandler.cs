// ClinicaPro.Core/Features/Medicos/Queries/ObterTodosMedicosQueryHandler.cs
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces; 

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // O Handler implementa IRequestHandler<Query, Resultado>
    public class ObterTodosMedicosQueryHandler : IRequestHandler<ObterTodosMedicosQuery, IEnumerable<Medico>>
    {
        // Injetamos a interface do Repositório (IMedicoRepository)
        private readonly IMedicoRepository _medicoRepository;

        public ObterTodosMedicosQueryHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IEnumerable<Medico>> Handle(ObterTodosMedicosQuery request, CancellationToken cancellationToken)
        {
            // Apenas repassa a chamada para o Repositório.
            return await _medicoRepository.GetAllAsync();
        }
    }
}