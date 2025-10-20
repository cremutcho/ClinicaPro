// ClinicaPro.Core/Features/Pacientes/Queries/ObterTodosPacientesQueryHandler.cs
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pacientes.Queries // PLURAL: Pacientes
{
    public class ObterTodosPacientesQueryHandler : IRequestHandler<ObterTodosPacientesQuery, IEnumerable<Paciente>>
    {
        // Injetamos a interface de Repositório que você implementou
        private readonly IPacienteRepository _pacienteRepository;

        public ObterTodosPacientesQueryHandler(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IEnumerable<Paciente>> Handle(ObterTodosPacientesQuery request, CancellationToken cancellationToken)
        {
            // Chamamos o método GetAllAsync() que está no seu IRepository genérico
            return await _pacienteRepository.GetAllAsync();
        }
    }
}