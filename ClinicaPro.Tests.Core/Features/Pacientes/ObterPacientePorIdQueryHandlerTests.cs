using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Pacientes.Queries;
using System;

namespace ClinicaPro.Tests.Core.Features.Pacientes
{
    public class ObterPacientePorIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarPaciente_QuandoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            const int pacienteId = 5;
            
            var pacienteEsperado = new Paciente 
            { 
                Id = pacienteId, 
                Nome = "Carlos Teste", 
                CPF = "999.999.999-99" 
            };

            mockRepo.Setup(repo => repo.GetByIdAsync(pacienteId))
                    .ReturnsAsync(pacienteEsperado);

            // 💡 CORREÇÃO: Usar inicializador de objeto para a propriedade Id
            var query = new ObterPacientePorIdQuery { Id = pacienteId }; 
            
            // NOTE: Você precisará criar ObterPacientePorIdQuery e Handler no ClinicaPro.Core
            var handler = new ObterPacientePorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(pacienteId), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal(pacienteEsperado.Id, resultado.Id);
            Assert.Equal(pacienteEsperado.Nome, resultado.Nome);
        }

        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoPacienteNaoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IPacienteRepository>();
            const int pacienteIdInexistente = 999; 

            mockRepo.Setup(repo => repo.GetByIdAsync(pacienteIdInexistente))
                    .ReturnsAsync((Paciente)null!);

            // 💡 CORREÇÃO: Usar inicializador de objeto para a propriedade Id
            var query = new ObterPacientePorIdQuery { Id = pacienteIdInexistente }; 
            var handler = new ObterPacientePorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(pacienteIdInexistente), Times.Once);
            Assert.Null(resultado);
        }
    }
}