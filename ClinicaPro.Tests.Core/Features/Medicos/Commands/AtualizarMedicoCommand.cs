using MediatR;
using ClinicaPro.Core.Entities; 

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    public record AtualizarMedicoCommand : IRequest<Unit>
    {
        // NOVO: Adicione 'required' para garantir que esta propriedade
        // seja inicializada no momento da criação do record (Command)
        public required Medico Medico { get; init; } 
    }
}