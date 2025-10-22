using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities; // Certifique-se de que a entidade Medico está aqui
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Commands;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class DeleteMedicoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarDeleteAsync_QuandoMedicoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            const int medicoIdToDelete = 101; 
            
            // 1. Simular o Médico que seria encontrado
            var medicoExistente = new Medico { Id = medicoIdToDelete, Nome = "Dr. Excluir" };

            // 2. CONFIGURAÇÃO CRÍTICA: Simular que o GetByIdAsync retorna o médico
            mockRepo.Setup(repo => repo.GetByIdAsync(medicoIdToDelete))
                    .ReturnsAsync(medicoExistente); // <-- O Handler vai buscar este objeto!
            
            // 3. Simular que o DeleteAsync retorna Task.CompletedTask
            mockRepo.Setup(repo => repo.DeleteAsync(medicoIdToDelete))
                    .Returns(Task.CompletedTask);

            var handler = new DeleteMedicoCommandHandler(mockRepo.Object);
            var command = new DeleteMedicoCommand(medicoIdToDelete);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 4. Verifica se a busca foi realizada UMA VEZ.
            mockRepo.Verify(repo => repo.GetByIdAsync(medicoIdToDelete), Times.Once);
            
            // 5. Verifica se o método DeleteAsync foi chamado UMA VEZ (após encontrar o médico).
            mockRepo.Verify(repo => repo.DeleteAsync(medicoIdToDelete), Times.Once);
        }
        
        [Fact]
        public async Task Handle_NaoDeveChamarDeleteAsync_QuandoMedicoNaoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            const int medicoIdToDelete = 999; 

            // 1. Configurar CRITICAMENTE o mock para retornar NULL (médico não encontrado)
            mockRepo.Setup(repo => repo.GetByIdAsync(medicoIdToDelete))
                    .ReturnsAsync((Medico)null!); // <--- Retorna null
            
            var handler = new DeleteMedicoCommandHandler(mockRepo.Object);
            var command = new DeleteMedicoCommand(medicoIdToDelete);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 2. Verifica se a busca foi realizada UMA VEZ.
            mockRepo.Verify(repo => repo.GetByIdAsync(medicoIdToDelete), Times.Once);
            
            // 3. Verifica se o método DeleteAsync NUNCA foi chamado.
            mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}