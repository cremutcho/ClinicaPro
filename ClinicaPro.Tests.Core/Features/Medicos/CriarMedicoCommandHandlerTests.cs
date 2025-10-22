using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Commands;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class CriarMedicoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarAddAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            
            // 1. Cria a entidade Medico para ser passada
            var novoMedico = new Medico 
            { 
                Id = 10,
                Nome = "Dr. Teste", 
                CRM = "CRM-998877",
                EspecialidadeId = 3,
                Email = "dr.teste@exemplo.com",
                Telefone = "11998877665",
            };

            // 2. Cria o Command usando o padrão de Entidade (Medico = novoMedico)
            var command = new CriarMedicoCommand { Medico = novoMedico }; 

            var handler = new CriarMedicoCommandHandler(mockRepo.Object);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None); // CancellationToken.None se AddAsync não aceitar

            // Assert (Verificação)
            
            // 3. Verifica se o método AddAsync foi chamado exatamente uma vez
            // e que a entidade passada é o novoMedico que criamos.
            mockRepo.Verify(repo => repo.AddAsync(It.Is<Medico>(
                 m => m.Nome == novoMedico.Nome && 
                      m.CRM == novoMedico.CRM &&
                      m.EspecialidadeId == novoMedico.EspecialidadeId
            )), Times.Once);
        }
    }
}