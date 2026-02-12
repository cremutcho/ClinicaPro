using System;

namespace ClinicaPro.Core.Features.RH.Queries
{
    // DTO (Data Transfer Object) para representar dados de funcionário em consultas
    // Alinhado com a entidade Funcionario (ID como int)
    public class FuncionarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public DateTime DataContratacao { get; set; }
        public bool Ativo { get; set; }
    }
}