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
    public class AtualizarPacienteCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarUpdateAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            const int pacienteId = 101;
            
            // 1. Dados para o Command
            const string novoNome = "Ana Nova Atualizada";
            const string novoEmail = "nova@email.com";
            var novaDataNascimento = new DateTime(1995, 5, 5);

            // 2. Simular que o UpdateAsync retorna sucesso
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Paciente>()))
                    .Returns(Task.CompletedTask); 
            
            // 3. Cria o objeto Paciente com os NOVOS VALORES para ser passado ao Command
            // (Note que o Handler deve usar o objeto Paciente inteiro do Command)
            var pacienteAtualizado = new Paciente
            {
                Id = pacienteId, 
                Nome = novoNome,
                CPF = "111.111.111-11", // Devemos passar todos os campos obrigatórios
                Email = novoEmail,
                DataNascimento = novaDataNascimento,
                Telefone = "999999999",
                Endereco = "Rua Nova"
            };

            // 4. Instanciar Command: O Command recebe o objeto Paciente
            var command = new AtualizarPacienteCommand { Paciente = pacienteAtualizado };
            
            // 5. Instanciar Handler (Apenas com IPacienteRepository, pois falhou com 2 argumentos)
            // Se o seu Handler usa IUnitOfWork, você deve adicioná-lo ao construtor e ao arquivo de teste.
            // Assumiremos que ele só precisa do Repo, por enquanto.
            var handler = new AtualizarPacienteCommandHandler(mockRepo.Object);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 6. Verificar APENAS se o método UpdateAsync foi chamado uma vez
            // E se o objeto passado para o UpdateAsync é o mesmo objeto que estava no Command.
            mockRepo.Verify(repo => repo.UpdateAsync(It.Is<Paciente>(p =>
                p.Id == pacienteId &&
                p.Nome == novoNome && 
                p.Email == novoEmail
            )), Times.Once);

            // 7. Não verificar GetByIdAsync ou Commit
        }
    }
}