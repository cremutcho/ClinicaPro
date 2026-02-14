using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Commands;
using MediatR;

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class CriarConsultaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarCriarAsync_SemConflito()
        {
            var mockService = new Mock<IConsultaService>();

            var novaConsulta = new Consulta
            {
                DataHora = DateTime.Now.AddDays(7),
                Status = StatusConsulta.Agendada,
                PacienteId = 1,
                MedicoId = 2
            };

            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            mockService.Setup(s => s.VerificaConflitoHorario(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int?>()
            )).ReturnsAsync(false);

            mockService.Setup(s => s.CriarAsync(
                    It.IsAny<Consulta>()
            )).ReturnsAsync(novaConsulta);

            var handler = new CriarConsultaCommandHandler(mockService.Object);

            await handler.Handle(command, CancellationToken.None);

            mockService.Verify(s => s.CriarAsync(
                It.Is<Consulta>(c =>
                    c.DataHora == novaConsulta.DataHora &&
                    c.PacienteId == novaConsulta.PacienteId &&
                    c.MedicoId == novaConsulta.MedicoId
                )
            ), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecao_QuandoHouverConflitoHorario()
        {
            var mockService = new Mock<IConsultaService>();

            var novaConsulta = new Consulta
            {
                DataHora = DateTime.Now.AddDays(7),
                Status = StatusConsulta.Agendada,
                PacienteId = 1,
                MedicoId = 2
            };

            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            mockService.Setup(s => s.VerificaConflitoHorario(
                    It.IsAny<int>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int?>()
            )).ReturnsAsync(true);

            var handler = new CriarConsultaCommandHandler(mockService.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(command, CancellationToken.None));

            mockService.Verify(s => s.CriarAsync(It.IsAny<Consulta>()), Times.Never);
        }
    }
}
