using Xunit;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Commands;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class AtualizarPacienteCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarAtualizarPacienteAsync_ComSucesso()
        {
            // Arrange
            var mockService = new Mock<IPacienteService>();
            const int pacienteId = 101;

            var pacienteAtualizado = new Paciente
            {
                Id = pacienteId,
                Nome = "Ana Nova Atualizada",
                CPF = "111.111.111-11",
                Email = "nova@email.com",
                DataNascimento = new DateTime(1995, 5, 5),
                Telefone = "999999999",
                Endereco = "Rua Nova"
            };

            var command = new AtualizarPacienteCommand { Paciente = pacienteAtualizado };

            // Configura o mock do Service com o nome correto do método
            mockService.Setup(s => s.AtualizarPacienteAsync(It.IsAny<Paciente>()))
                       .Returns(Task.CompletedTask);

            var handler = new AtualizarPacienteCommandHandler(mockService.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert: verifica se o método correto foi chamado
            mockService.Verify(s => s.AtualizarPacienteAsync(It.Is<Paciente>(p =>
                p.Id == pacienteId &&
                p.Nome == pacienteAtualizado.Nome &&
                p.Email == pacienteAtualizado.Email
            )), Times.Once);
        }
    }
}
