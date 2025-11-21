using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDatasFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- AJUSTE DO CAMPO ATIVO (string -> bool) ---
            migrationBuilder.Sql("UPDATE Funcionarios SET Ativo = 'true' WHERE Ativo = 'sim'");
            migrationBuilder.Sql("UPDATE Funcionarios SET Ativo = 'false' WHERE Ativo = 'não' OR Ativo = 'nao' OR Ativo = '' OR Ativo IS NULL");

            // --- AJUSTE DAS DATAS (string -> datetime2) ---
            // Caso haja datas inválidas, converte para uma data padrão
            migrationBuilder.Sql(@"
                UPDATE Funcionarios
                SET DataNascimento = '2000-01-01'
                WHERE ISDATE(DataNascimento) = 0 OR DataNascimento IS NULL OR DataNascimento = '';
            ");

            migrationBuilder.Sql(@"
                UPDATE Funcionarios
                SET DataAdmissao = GETDATE()
                WHERE ISDATE(DataAdmissao) = 0 OR DataAdmissao IS NULL OR DataAdmissao = '';
            ");

            // --- ALTERAÇÃO DAS COLUNAS ---
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Funcionarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAdmissao",
                table: "Funcionarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Funcionarios",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataNascimento",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DataAdmissao",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Ativo",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
