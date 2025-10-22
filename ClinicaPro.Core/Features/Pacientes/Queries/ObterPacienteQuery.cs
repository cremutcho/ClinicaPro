using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Pacientes.Queries
{
    // A Query recebe o ID (parâmetro de entrada) e retorna um objeto Paciente (ou null)
    // O record é uma forma concisa de criar classes imutáveis (read-only)
    public record ObterPacienteQuery(int Id) : IRequest<Paciente?>;
}