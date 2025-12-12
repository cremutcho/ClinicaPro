using Moq;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Commands;

// Adicione o using para o seu enum StatusConsulta (ou classe de valor)
// Exemplo: using ClinicaPro.Core.Entities.Enums; 


namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class CriarConsultaCommandHandlerTests
    {
        
        [Fact]
        public async Task Handle_DeveChamarAddAsync_SemConflito()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            
            // 1. MOCK: Configura o repositório para RETORNAR FALSO (SEM CONFLITO)
            mockRepo.Setup(repo => 
                repo.VerificaConflitoHorario(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false); 

            // 2. Cria a entidade de teste que será passada ao Command
            var novaConsulta = new Consulta 
            { 
                DataHora = DateTime.Now.AddDays(7), 
                Status = StatusConsulta.Agendada, // Assumindo StatusConsulta.Agendada
                PacienteId = 1, 
                MedicoId = 2 
            };

            var handler = new CriarConsultaCommandHandler(mockRepo.Object);
            var command = new CriarConsultaCommand { Consulta = novaConsulta };

            // Act (Ação)
            await handler.Handle(command, CancellationToken.None);

            // Assert (Verificação)
            
            // 3. Verifica se o método AddAsync foi chamado UMA VEZ.
            mockRepo.Verify(repo => repo.AddAsync(It.Is<Consulta>(c => 
                c.DataHora == novaConsulta.DataHora)), 
                Times.Once);
        }

        // ------------------------------------------------------------------
        // TESTE 2: CENÁRIO DE ERRO (COM CONFLITO)
        // ------------------------------------------------------------------
        [Fact]
        public async Task Handle_DeveLancarExcecao_QuandoHouverConflitoHorario()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            
            // 1. MOCK: Configura o repositório para RETORNAR TRUE (COM CONFLITO)
            mockRepo.Setup(repo => 
                repo.VerificaConflitoHorario(It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true); 

            var novaConsulta = new Consulta 
            { 
                DataHora = DateTime.Now.AddDays(7), 
                Status = StatusConsulta.Agendada,
                PacienteId = 1, 
                MedicoId = 2 
            };
            var command = new CriarConsultaCommand { Consulta = novaConsulta };
            var handler = new CriarConsultaCommandHandler(mockRepo.Object);

            // Act & Assert (Ação e Verificação)
            // 2. Verifica se a exceção correta é lançada
            await Assert.ThrowsAsync<InvalidOperationException>(() => 
                handler.Handle(command, CancellationToken.None));

            // 3. Verifica que AddAsync NUNCA foi chamado (pois o processo falhou antes)
            mockRepo.Verify(repo => 
                repo.AddAsync(It.IsAny<Consulta>()), 
                Times.Never);
        }
    }
}