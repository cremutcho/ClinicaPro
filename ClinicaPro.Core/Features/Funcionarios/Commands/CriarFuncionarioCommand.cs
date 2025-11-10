using MediatR;
using ClinicaPro.Core.Entities;
using System;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    // O Command usa um DTO simples (o próprio record) para transportar os dados
    // Ele retorna o ID do novo Funcionário (int) após a criação.
    public record CriarFuncionarioCommand(
        string Nome,
        string Sobrenome,
        string CPF,
        DateTime DataContratacao,
        string Cargo,
        bool Ativo = true // Valor padrão
    ) : IRequest<int>;
}