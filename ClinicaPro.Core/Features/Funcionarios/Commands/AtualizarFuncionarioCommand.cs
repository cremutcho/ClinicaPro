using MediatR;
using System;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    // O Command agora inclui o Id do registro a ser atualizado
    public record AtualizarFuncionarioCommand(
        int Id, // CHAVE: ID OBRIGATÓRIO
        string Nome,
        string Sobrenome,
        string CPF,
        DateTime DataContratacao,
        string Cargo,
        bool Ativo
    ) : IRequest<Unit>; // Unit é usado quando o Command não precisa retornar dados (apenas sucesso)
}