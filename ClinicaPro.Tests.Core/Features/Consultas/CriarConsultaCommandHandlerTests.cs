using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Commands;
using System;

// Adicione o using para o seu enum StatusConsulta (ou classe de valor)
// Assumindo que está dentro de ClinicaPro.Core.Entities
// Se o enum estiver em outro lugar, ajuste este using:
using ClinicaPro.Core.Entities; 

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class CriarConsultaCommandHandlerTests
    {
        [Fact]
        public async Task Handle_DeveChamarAddAsyncESaveChangesAsync_ComSucesso()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            
            // 2. Cria a entidade de teste que será passada ao Command
            var novaConsulta = new Consulta 
            { 
                DataHora = DateTime.Now.AddDays(7), 
                // ✅ CORRIGIDO: Use o Enum StatusConsulta em vez de string literal
                // Assumindo que seu enum tem um valor chamado 'Agendada'
                Status = StatusConsulta.Agendada, 
                PacienteId = 1, 
                MedicoId = 2 
            };

            var handler = new CriarConsultaCommandHandler(mockRepo.Object);
            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // Verifica se o método AddAsync foi chamado UMA VEZ com a entidade de consulta.
            mockRepo.Verify(repo => repo.AddAsync(It.Is<Consulta>(c => 
                c.DataHora == novaConsulta.DataHora && c.Status == StatusConsulta.Agendada)), 
                Times.Once);
        }
    }
}