// ClinicaPro.Core/Features/Medicos/Queries/MedicoExisteQueryHandler.cs
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    public class MedicoExisteQueryHandler : IRequestHandler<MedicoExisteQuery, bool>
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoExisteQueryHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<bool> Handle(MedicoExisteQuery request, CancellationToken cancellationToken)
        {
            // O repositório precisa de um método que verifique a existência, geralmente usando AnyAsync.
            // Se o seu IMedicoRepository não tiver um método específico de verificação,
            // você precisará adicioná-lo ou, temporariamente, fazer uma busca leve.
            
            // Supondo que você adicione um método 'ExistsAsync(int id)' ao IMedicoRepository
            // return await _medicoRepository.ExistsAsync(request.Id); 
            
            // Se o repositório só tem o GetByIdAsync, fazemos a checagem com ele:
            var medico = await _medicoRepository.GetByIdAsync(request.Id);
            return medico != null;
        }
    }
}