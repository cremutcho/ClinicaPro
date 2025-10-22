using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Queries;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class ObterPacienteQueryHandlerTests
    {
        private readonly Mock<IPacienteRepository> _mockRepo;
        private readonly ObterPacienteQueryHandler _handler;

        public ObterPacienteQueryHandlerTests()
        {
            // Inicializa o mock do repositório
            _mockRepo = new Mock<IPacienteRepository>();
            // Cria a instância do Handler com o mock
            _handler = new ObterPacienteQueryHandler(_mockRepo.Object);
        }

        // --- TESTE DE SUCESSO ---
        [Fact]
        public async Task Handle_DeveRetornarPaciente_QuandoEncontrado()
        {
            // Arrange (Preparação)
            const int pacienteId = 15;
            
            // Cria um Paciente simulado que o repositório deve retornar
            var pacienteEsperado = new Paciente 
            { 
                Id = pacienteId, 
                Nome = "Ana Silva", 
                CPF = "12345678900" 
            };

            // Configura o mock: Quando GetByIdAsync for chamado com o ID, retorne pacienteEsperado
            _mockRepo.Setup(repo => repo.GetByIdAsync(
                pacienteId)) // <--- CORRIGIDO: Removido It.IsAny<CancellationToken>()
                .ReturnsAsync(pacienteEsperado);

            var query = new ObterPacienteQuery(pacienteId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            // 1. O resultado não deve ser nulo
            Assert.NotNull(resultado);
            // 2. Verifica se a ID é a correta
            Assert.Equal(pacienteId, resultado.Id);
            // 3. Verifica se o método do repositório foi chamado UMA VEZ
            _mockRepo.Verify(repo => repo.GetByIdAsync(pacienteId), Times.Once); // <--- CORRIGIDO: Removido It.IsAny<CancellationToken>()
        }

        // --- TESTE DE FALHA (NÃO ENCONTRADO) ---
        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoNaoEncontrado()
        {
            // Arrange (Preparação)
            const int pacienteId = 99;
            
            // Configura o mock: Quando GetByIdAsync for chamado, retorne null
            _mockRepo.Setup(repo => repo.GetByIdAsync(
                pacienteId)) // <--- CORRIGIDO: Removido It.IsAny<CancellationToken>()
                .ReturnsAsync((Paciente?)null); // Retorna nulo

            var query = new ObterPacienteQuery(pacienteId);

            // Act (Ação)
            var resultado = await _handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            // 1. O resultado deve ser nulo
            Assert.Null(resultado);
            // 2. Verifica se o método do repositório foi chamado UMA VEZ
            _mockRepo.Verify(repo => repo.GetByIdAsync(pacienteId), Times.Once); // <--- CORRIGIDO: Removido It.IsAny<CancellationToken>()
        }
    }
}