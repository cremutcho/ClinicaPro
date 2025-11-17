using MediatR;
using System;

namespace ClinicaPro.Core.Features.RH.Commands
{
    // O Command é o DTO que carrega os dados de entrada para a ação de criação
    public class CriarFuncionarioCommand : IRequest<int>
    {
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
    
        public DateTime DataContratacao { get; set; } = DateTime.Now;
    }
}