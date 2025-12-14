using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Commands;
using System;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class CriarPacienteCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarCriarAsync_ComSucesso()
        {
            // Arrange
            var mockService = new Mock<IPacienteService>();

            var novoPaciente = new Paciente
            {
                Id = 10,
                Nome = "Ana Teste",
                DataNascimento = new DateTime(1995, 8, 20),
                CPF = "123.456.789-00",
                Telefone = "11998877665",
                Email = "ana.teste@exemplo.com",
                Endereco = "Rua Teste, 123"
            };

            var command = new CriarPacienteCommand
            {
                Paciente = novoPaciente
            };

            // Configura o mock para retornar o próprio paciente
            mockService
                .Setup(service => service.CriarAsync(It.IsAny<Paciente>()))
                .ReturnsAsync(novoPaciente);

            var handler = new CriarPacienteCommandHandler(mockService.Object);

            // Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockService.Verify(service =>
                service.CriarAsync(It.Is<Paciente>(p =>
                    p.Nome == novoPaciente.Nome &&
                    p.CPF == novoPaciente.CPF
                )),
                Times.Once
            );

            Assert.Equal(novoPaciente, resultado);
        }
    }
}
