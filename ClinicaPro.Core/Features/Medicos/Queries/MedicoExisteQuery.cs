// ClinicaPro.Core/Features/Medicos/Queries/MedicoExisteQuery.cs
using MediatR;

namespace ClinicaPro.Core.Features.Medicos.Queries
{
    // Retorna um booleano
    public record MedicoExisteQuery(int Id) : IRequest<bool>;
}