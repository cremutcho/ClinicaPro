using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Queries;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class ObterMedicoQueryHandlerTests
    {
        private readonly Mock<IMedicoRepository> _mockRepo;
        private readonly ObterMedicoQueryHandler _handler;

        public ObterMedicoQueryHandlerTests()
        {
            _mockRepo = new Mock<IMedicoRepository>();
            _handler = new ObterMedicoQueryHandler(_mockRepo.Object);
        }

        // --- TESTE DE SUCESSO ---
        [Fact]
        public async Task Handle_DeveRetornarMedico_QuandoEncontrado()
        {
            // Arrange (Preparação)
            const int medicoId = 5;
            
            var medicoEsperado = new Medico 
            { 
                Id = medicoId, 
                Nome = "Dr. Souza", 
                CRM = "123456",
                EspecialidadeId = 1
            };

            // Configura o mock (Setup) - SEM CancellationToken
            _mockRepo.Setup(repo => repo.GetByIdAsync(medicoId))
                .ReturnsAsync(medicoEsperado);

            var query = new ObterMedicoQuery(medicoId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            Assert.NotNull(resultado);
            Assert.Equal(medicoId, resultado.Id);
            Assert.Equal("Dr. Souza", resultado.Nome);
            
            // Verifica se o método do repositório foi chamado (Verify) - SEM CancellationToken
            _mockRepo.Verify(repo => repo.GetByIdAsync(medicoId), Times.Once);
        }

        // --- TESTE DE FALHA (NÃO ENCONTRADO) ---
        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoNaoEncontrado()
        {
            // Arrange (Preparação)
            const int medicoId = 99;
            
            // Configura o mock (Setup) - SEM CancellationToken
            _mockRepo.Setup(repo => repo.GetByIdAsync(medicoId))
                .ReturnsAsync((Medico?)null); 

            var query = new ObterMedicoQuery(medicoId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            Assert.Null(resultado);
            
            // Verifica se o método do repositório foi chamado (Verify) - SEM CancellationToken
            _mockRepo.Verify(repo => repo.GetByIdAsync(medicoId), Times.Once);
        }
    }
}