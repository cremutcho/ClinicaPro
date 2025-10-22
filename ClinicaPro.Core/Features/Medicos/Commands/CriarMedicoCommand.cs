using MediatR;
using ClinicaPro.Core.Entities; // <-- ESSENCIAL: Garante que a classe Medico é reconhecida

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O command representa o dado necessário para criar um Medico.
    public record CriarMedicoCommand : IRequest<int>
    {
        // NOVO PADRÃO: Carrega a entidade Medico completa, seguindo o padrão de Paciente.
        // O validador agora irá validar as propriedades DENTRO desta entidade.
        public required Medico Medico { get; init; } 
    }
}