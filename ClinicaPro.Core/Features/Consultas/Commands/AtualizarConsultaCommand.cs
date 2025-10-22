using MediatR;
using ClinicaPro.Core.Entities;
// Certifique-se de que a entidade Consulta está sendo importada

namespace ClinicaPro.Core.Features.Consultas.Commands
{
    // O comando implementa IRequest<Unit> porque ele apenas executa uma ação
    // e não retorna dados (Unit é um tipo de valor vazio do MediatR)
    public record AtualizarConsultaCommand : IRequest<Unit>
    {
        // Esta propriedade contém os novos dados da consulta
        public required Consulta Consulta { get; init; } 
    }
}