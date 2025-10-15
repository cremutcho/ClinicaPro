// ClinicaPro.Core/Features/Medicos/Queries/ObterMedicoPorIdQuery.cs
using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // A Query precisa do ID e deve retornar um único Medico (ou null).
    public record ObterMedicoPorIdQuery(int Id) : IRequest<Medico?>; // CORRIGIDO: Adicionado '?'
}