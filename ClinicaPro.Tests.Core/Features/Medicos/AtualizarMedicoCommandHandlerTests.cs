using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Commands; // AQUI ESTÁ A CHAVE!
using System;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class AtualizarMedicoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarUpdateAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            const int medicoId = 7;
            
            const string novoNome = "Dr. Roberto Atualizado";
            const string novoCRM = "RJ987654";

            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Medico>()))
                    .Returns(Task.CompletedTask); 
            
            // 💡 CORREÇÃO: Se Especialidade é uma classe/record e não tem construtor simples,
            // ou se o tipo é complexo, usamos 'default!' (para non-nullables) ou 'null!' 
            // no contexto de teste. Vamos assumir que é um tipo de referência.
            var medicoAtualizado = new Medico
            {
                Id = medicoId, 
                Nome = novoNome,
                CRM = novoCRM, 
                Especialidade = default! // Usamos default! para satisfazer o tipo de referência
            };

            // O Command agora deve ser encontrado após a criação no Core
            var command = new AtualizarMedicoCommand { Medico = medicoAtualizado };
            
            // O Handler agora deve ser encontrado após a criação no Core
            var handler = new AtualizarMedicoCommandHandler(mockRepo.Object);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // Verificar se o método UpdateAsync foi chamado uma vez
            mockRepo.Verify(repo => repo.UpdateAsync(It.Is<Medico>(m =>
                m.Id == medicoId &&
                m.Nome == novoNome && 
                m.CRM == novoCRM
            )), Times.Once);
        }
    }
}