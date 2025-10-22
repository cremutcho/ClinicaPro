// Código do teste (Certifique-se de que este é o que você está usando)
using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Consultas.Queries;
using System;

namespace ClinicaPro.Tests.Core.Features.Consultas.Queries
{
    public class ObterConsultaPorIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarConsulta_QuandoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            const int consultaId = 12;
            
            // 1. A consulta esperada (SEM A PROPRIEDADE 'Descricao')
            var consultaEsperada = new Consulta 
            { 
                Id = consultaId, 
                DataHora = new DateTime(2025, 1, 10, 10, 0, 0),
                MedicoId = 1,
                PacienteId = 5
            };

            // 2. Configurar o mock
            mockRepo.Setup(repo => repo.GetByIdAsync(consultaId))
                    .ReturnsAsync(consultaEsperada);

            // 3. Instanciar Handler e Query
            var query = new ObterConsultaPorIdQuery(consultaId); 
            var handler = new ObterConsultaPorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(consultaId), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal(consultaEsperada.Id, resultado.Id);
            Assert.Equal(consultaEsperada.MedicoId, resultado.MedicoId);
            Assert.Equal(consultaEsperada.PacienteId, resultado.PacienteId);
            // NÃO HÁ VERIFICAÇÃO DE 'Descricao' AQUI
        }

        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoConsultaNaoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IConsultaRepository>();
            const int consultaIdInexistente = 999; 

            mockRepo.Setup(repo => repo.GetByIdAsync(consultaIdInexistente))
                    .ReturnsAsync((Consulta)null!);

            // 2. Instanciar Handler e Query
            var query = new ObterConsultaPorIdQuery(consultaIdInexistente); 
            var handler = new ObterConsultaPorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(consultaIdInexistente), Times.Once);
            Assert.Null(resultado);
        }
    }
}