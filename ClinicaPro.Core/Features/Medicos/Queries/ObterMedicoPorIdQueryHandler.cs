// ClinicaPro.Core/Features/Medicos/Queries/ObterMedicoPorIdQueryHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // O Handler implementa IRequestHandler<Query, Resultado>
    // CORRIGIDO: O tipo de retorno agora é Medico?
    public class ObterMedicoPorIdQueryHandler : IRequestHandler<ObterMedicoPorIdQuery, Medico?>
    {
        // Injetamos a interface do Repositório (IMedicoRepository)
        private readonly IMedicoRepository _medicoRepository;

        public ObterMedicoPorIdQueryHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        // CORRIGIDO: O método Handle agora retorna Task<Medico?>
        public async Task<Medico?> Handle(ObterMedicoPorIdQuery request, CancellationToken cancellationToken)
        {
            // O Handler faz a chamada direta ao Repositório para obter os dados.
            // O GetByIdAsync é definido para retornar um objeto anulável (Medico?).
            return await _medicoRepository.GetByIdAsync(request.Id);
        }
    }
}