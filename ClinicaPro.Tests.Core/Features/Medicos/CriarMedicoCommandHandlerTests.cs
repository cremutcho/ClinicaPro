using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Commands;
using MediatR; // Para Unit

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class CriarMedicoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarCriarAsync_ComSucesso()
        {
            // Arrange
            var mockService = new Mock<IMedicoService>();

            var novoMedico = new Medico
            {
                Id = 10,
                Nome = "Dr. Teste",
                CRM = "CRM-998877",
                EspecialidadeId = 3,
                Email = "dr.teste@exemplo.com",
                Telefone = "11998877665",
            };

            var command = new CriarMedicoCommand { Medico = novoMedico };

            // Configura o mock: apenas retorna Unit.Value, pois o método não precisa retornar nada
            mockService.Setup(s => s.CriarAsync(It.IsAny<Medico>()))
                       .Returns(Task.FromResult(novoMedico)); // ou Task.CompletedTask se o handler não usar o retorno

            var handler = new CriarMedicoCommandHandler(mockService.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            // Verifica se o método CriarAsync foi chamado exatamente uma vez
            mockService.Verify(s => s.CriarAsync(It.Is<Medico>(m =>
                m.Nome == novoMedico.Nome &&
                m.CRM == novoMedico.CRM &&
                m.EspecialidadeId == novoMedico.EspecialidadeId
            )), Times.Once);
        }
    }
}
