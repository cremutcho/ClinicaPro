using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Commands;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class DeletarPacienteCommandHandlerTests 
    {
        // Teste 1: O Paciente Existe (A exclusão deve ser chamada)
        [Fact]
        public async Task Handle_DeveChamarDeleteAsync_SempreQueComandoForExecutado()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            
            const int pacienteIdToDelete = 202; 
            
            // Configurar o DeleteAsync para retornar uma Task concluída
            mockRepo.Setup(repo => repo.DeleteAsync(pacienteIdToDelete))
                    .Returns(Task.CompletedTask); 
            
            // **IMPORTANTE:** Removeremos a configuração do GetByIdAsync

            var handler = new DeletarPacienteCommandHandler(mockRepo.Object /* e outros mocks, se houver */);
            var command = new DeletarPacienteCommand { Id = pacienteIdToDelete };

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 💡 Verificar APENAS se o método DeleteAsync foi chamado UMA VEZ.
            mockRepo.Verify(repo => repo.DeleteAsync(pacienteIdToDelete), Times.Once);

            // 💡 Removeremos a verificação do GetByIdAsync
        }
        
        // Teste 2: O Paciente Não Existe (O repositório ainda é chamado, mas não deve falhar)
        [Fact]
        public async Task Handle_AindaDeveTentarChamarDeleteAsync_QuandoIdInexistente()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            const int pacienteIdToDelete = 999; 

            // Configurar o DeleteAsync para retornar uma Task concluída
            mockRepo.Setup(repo => repo.DeleteAsync(pacienteIdToDelete))
                    .Returns(Task.CompletedTask);

            // **IMPORTANTE:** Removeremos a configuração do GetByIdAsync
            
            var handler = new DeletarPacienteCommandHandler(mockRepo.Object /* e outros mocks, se houver */);
            var command = new DeletarPacienteCommand { Id = pacienteIdToDelete };

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 💡 Verificar APENAS se o método DeleteAsync foi chamado UMA VEZ.
            // O repositório lida com o erro de ID inexistente.
            mockRepo.Verify(repo => repo.DeleteAsync(pacienteIdToDelete), Times.Once);

            // 💡 Removeremos a verificação do GetByIdAsync
        }
    }
}