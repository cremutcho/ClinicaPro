using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Commands;
using MediatR; // Para Unit

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class CriarConsultaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarCriarAsync_SemConflito()
        {
            // Arrange
            var mockService = new Mock<IConsultaService>();

            var novaConsulta = new Consulta
            {
                DataHora = DateTime.Now.AddDays(7),
                Status = StatusConsulta.Agendada,
                PacienteId = 1,
                MedicoId = 2
            };

            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            // Mock do Service
            mockService.Setup(s => s.VerificaConflitoHorario(It.IsAny<int>(), It.IsAny<DateTime>()))
                       .ReturnsAsync(false);

            mockService.Setup(s => s.CriarAsync(It.IsAny<Consulta>()))
                       .Returns(Task.FromResult(novaConsulta)); // retorna a mesma consulta, mas teste não acessa

            var handler = new CriarConsultaCommandHandler(mockService.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockService.Verify(s => s.CriarAsync(It.Is<Consulta>(c =>
                c.DataHora == novaConsulta.DataHora &&
                c.PacienteId == novaConsulta.PacienteId &&
                c.MedicoId == novaConsulta.MedicoId
            )), Times.Once);
        }

        [Fact]
        public async Task Handle_DeveLancarExcecao_QuandoHouverConflitoHorario()
        {
            // Arrange
            var mockService = new Mock<IConsultaService>();

            var novaConsulta = new Consulta
            {
                DataHora = DateTime.Now.AddDays(7),
                Status = StatusConsulta.Agendada,
                PacienteId = 1,
                MedicoId = 2
            };

            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            // Mock do Service: retorna conflito
            mockService.Setup(s => s.VerificaConflitoHorario(It.IsAny<int>(), It.IsAny<DateTime>()))
                       .ReturnsAsync(true);

            var handler = new CriarConsultaCommandHandler(mockService.Object);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(command, CancellationToken.None));

            mockService.Verify(s => s.CriarAsync(It.IsAny<Consulta>()), Times.Never);
        }
    }
}
