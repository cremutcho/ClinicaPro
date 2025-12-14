using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Pacientes.Commands;
using ClinicaPro.Core.Interfaces;
using MediatR;

public class CriarPacienteCommandHandler
    : IRequestHandler<CriarPacienteCommand, Paciente>
{
    private readonly IPacienteService _pacienteService;

    public CriarPacienteCommandHandler(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    public async Task<Paciente> Handle(
        CriarPacienteCommand request,
        CancellationToken cancellationToken)
    {
        return await _pacienteService.CriarAsync(request.Paciente);
    }
}
