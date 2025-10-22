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
    public class AtualizarConsultaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarUpdateAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            const int consultaId = 15;
            
            // 1. Novos dados para a atualização
            var novaDataHora = new DateTime(2025, 1, 15, 14, 30, 0);
            const int novoMedicoId = 2; // Mudança de médico
            const int novoPacienteId = 8; // Mudança de paciente 

            // 2. Simular que o UpdateAsync retorna sucesso
            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Consulta>()))
                    .Returns(Task.CompletedTask); 
            
            // 3. Cria o objeto Consulta com os NOVOS VALORES
            var consultaAtualizada = new Consulta
            {
                Id = consultaId, 
                DataHora = novaDataHora,
                MedicoId = novoMedicoId, 
                PacienteId = novoPacienteId,
                // !!! A PROPRIEDADE 'Resultado' FOI REMOVIDA PARA CORRIGIR O ERRO CS0117 !!!
            };

            // 4. Instanciar Command e Handler
            var command = new AtualizarConsultaCommand { Consulta = consultaAtualizada };
            var handler = new AtualizarConsultaCommandHandler(mockRepo.Object);

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 5. Verificar se o método UpdateAsync foi chamado uma vez
            mockRepo.Verify(repo => repo.UpdateAsync(It.Is<Consulta>(c =>
                c.Id == consultaId &&
                c.DataHora == novaDataHora && 
                c.MedicoId == novoMedicoId &&
                c.PacienteId == novoPacienteId
                // !!! NENHUMA VERIFICAÇÃO DE 'Resultado' AQUI !!!
            )), Times.Once);
        }
    }
}