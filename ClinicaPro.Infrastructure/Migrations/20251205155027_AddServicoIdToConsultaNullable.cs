using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServicoIdToConsultaNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas");

            migrationBuilder.AlterColumn<int>(
                name: "ServicoId",
                table: "Consultas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas");

            migrationBuilder.AlterColumn<int>(
                name: "ServicoId",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
