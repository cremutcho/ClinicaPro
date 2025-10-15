using MediatR;

namespace ClinicaPro.Core.Features.Medicos.Commands
{
    // O command representa o dado necessário para criar um Medico.
    public record CriarMedicoCommand : IRequest<int>
    {
        // Adicione 'required' para garantir que não sejam nulos ao sair do construtor
        public required string Nome { get; init; } 
        public required string CRM { get; init; } 
        public int EspecialidadeId { get; init; } // Não precisa ser 'required' se for 0 por padrão ou for validado de outra forma
        public required string Email { get; init; }
        public required string Telefone { get; init; }
    }
}