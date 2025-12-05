using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServicoIdToConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicoId",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ServicoId",
                table: "Consultas",
                column: "ServicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas",
                column: "ServicoId",
                principalTable: "Servicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Servicos_ServicoId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_ServicoId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "ServicoId",
                table: "Consultas");
        }
    }
}
