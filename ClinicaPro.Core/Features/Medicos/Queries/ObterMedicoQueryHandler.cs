using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // O Handler aceita a ObterMedicoQuery e retorna Medico? (Medico ou nulo)
    public class ObterMedicoQueryHandler : IRequestHandler<ObterMedicoQuery, Medico?>
    {
        private readonly IMedicoRepository _medicoRepository;

        // Injeção de Dependência
        public ObterMedicoQueryHandler(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Medico?> Handle(ObterMedicoQuery request, CancellationToken cancellationToken)
        {
            // Delega a busca ao repositório (sem CancellationToken, conforme sua interface)
            return await _medicoRepository.GetByIdAsync(request.Id);
        }
    }
}