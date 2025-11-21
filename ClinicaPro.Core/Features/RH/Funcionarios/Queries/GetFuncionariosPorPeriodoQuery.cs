using MediatR;
using ClinicaPro.Core.Entities;
using System;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorPeriodo
{
    public class GetFuncionariosPorPeriodoQuery : IRequest<IEnumerable<Funcionario>>
    {
        public DateTime DataInicio { get; }
        public DateTime DataFim { get; }

        public GetFuncionariosPorPeriodoQuery(DateTime dataInicio, DateTime dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
    }
}
