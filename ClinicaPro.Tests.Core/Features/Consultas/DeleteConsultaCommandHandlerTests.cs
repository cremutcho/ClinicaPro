using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Commands;
using System;

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class DeleteConsultaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarDeleteAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            const int consultaIdToDelete = 5; // ID fictício para exclusão

            // 1. Configurar o mock para o método DeleteAsync, garantindo que ele será chamado
            // Note: Não precisamos configurar o retorno se o método for 'void' ou retornar Task.
            
            // 2. Criar o Handler
            var handler = new DeleteConsultaCommandHandler(mockRepo.Object);
            
            // 3. Criar o Command
            var command = new DeleteConsultaCommand(consultaIdToDelete);

            // Act (Ação)
            // O Handle deve ser executado sem lançar exceção
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 4. Verifica se o método DeleteAsync foi chamado exatamente uma vez com o ID correto.
            // Isso confirma que o Handler cumpriu sua responsabilidade de exclusão.
            mockRepo.Verify(repo => repo.DeleteAsync(consultaIdToDelete), Times.Once);
        }
    }
}