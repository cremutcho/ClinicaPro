using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Features.Medicos.Queries;
using System;

namespace ClinicaPro.Tests.Core.Features.Medicos
{
    public class ObterMedicoPorIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_DeveRetornarMedico_QuandoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            const int medicoId = 10;
            
            var medicoEsperado = new Medico 
            { 
                Id = medicoId, 
                Nome = "Dr. João Silva", 
                CRM = "SP123456" 
            };

            mockRepo.Setup(repo => repo.GetByIdAsync(medicoId))
                    .ReturnsAsync(medicoEsperado);

            // 💡 CORREÇÃO: Instanciar a Query usando o CONSTRUTOR com o parâmetro 'Id'
            var query = new ObterMedicoPorIdQuery(medicoId); 
            var handler = new ObterMedicoPorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(medicoId), Times.Once);
            Assert.NotNull(resultado);
            Assert.Equal(medicoEsperado.Id, resultado.Id);
            Assert.Equal(medicoEsperado.Nome, resultado.Nome);
            Assert.Equal(medicoEsperado.CRM, resultado.CRM);
        }

        [Fact]
        public async Task Handle_DeveRetornarNulo_QuandoMedicoNaoExiste()
        {
            // Arrange (Preparação)
            var mockRepo = new Mock<IMedicoRepository>();
            const int medicoIdInexistente = 999; 

            mockRepo.Setup(repo => repo.GetByIdAsync(medicoIdInexistente))
                    .ReturnsAsync((Medico)null!);

            // 💡 CORREÇÃO: Instanciar a Query usando o CONSTRUTOR
            var query = new ObterMedicoPorIdQuery(medicoIdInexistente); 
            var handler = new ObterMedicoPorIdQueryHandler(mockRepo.Object);

            // Act (Ação)
            var resultado = await handler.Handle(query, CancellationToken.None);

            // Assert (Verificação)
            mockRepo.Verify(repo => repo.GetByIdAsync(medicoIdInexistente), Times.Once);
            Assert.Null(resultado);
        }
    }
}