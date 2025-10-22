using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Queries;
using System;

// Lembre-se de adicionar o using para o seu enum StatusConsulta
// Exemplo: using ClinicaPro.Core.Entities.Enums; 

namespace ClinicaPro.Tests.Core.Features.Consultas
{
    public class ObterConsultaQueryHandlerTests
    {
        private readonly Mock<IConsultaRepository> _mockRepo;
        private readonly ObterConsultaQueryHandler _handler;

        public ObterConsultaQueryHandlerTests()
        {
            _mockRepo = new Mock<IConsultaRepository>();
            _handler = new ObterConsultaQueryHandler(_mockRepo.Object);
        }

        // --- TESTE DE SUCESSO ---
        [Fact]
        public async Task Handle_DeveRetornarConsulta_QuandoEncontrada()
        {
            // Arrange (Preparação)
            const int consultaId = 10;
            
            var consultaEsperada = new Consulta 
            { 
                Id = consultaId, 
                DataHora = DateTime.Now.AddDays(1),
                MedicoId = 2,
                PacienteId = 5,
                Status = StatusConsulta.Agendada // Use o Enum correto aqui
            };

            // Configura o mock (Setup) - SEM CancellationToken
            _mockRepo.Setup(repo => repo.GetByIdAsync(consultaId))
                .ReturnsAsync(consultaEsperada);

            var query = new ObterConsultaQuery(consultaId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal(consultaId, resultado.Id);
            // Verifica se o método do repositório foi chamado (Verify) - SEM CancellationToken
            _mockRepo.Verify(repo => repo.GetByIdAsync(consultaId), Times.Once);
        }

        // --- TESTE DE FALHA (NÃO ENCONTRADO) ---
        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoNaoEncontrada()
        {
            // Arrange (Preparação)
            const int consultaId = 99;
            
            // Configura o mock (Setup) - SEM CancellationToken
            _mockRepo.Setup(repo => repo.GetByIdAsync(consultaId))
                .ReturnsAsync((Consulta?)null); 

            var query = new ObterConsultaQuery(consultaId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            Assert.Null(resultado);
            
            // Verifica se o método do repositório foi chamado (Verify) - SEM CancellationToken
            _mockRepo.Verify(repo => repo.GetByIdAsync(consultaId), Times.Once);
        }
    }
}