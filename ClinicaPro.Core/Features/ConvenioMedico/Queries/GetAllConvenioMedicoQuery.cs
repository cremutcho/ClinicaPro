// ClinicaPro.Core/Features/ConvenioMedico/Queries/GetAllConvenioMedicoQuery.cs
using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.ConvenioMedico.Queries
{
    public record GetAllConvenioMedicoQuery() 
    : IRequest<List<ClinicaPro.Core.Entities.ConvenioMedico>>;
}
