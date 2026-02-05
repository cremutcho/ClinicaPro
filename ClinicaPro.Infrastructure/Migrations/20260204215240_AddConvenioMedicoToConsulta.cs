using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConvenioMedicoToConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConvenioMedicoId",
                table: "Consultas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ConvenioMedicoId",
                table: "Consultas",
                column: "ConvenioMedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_ConveniosMedicos_ConvenioMedicoId",
                table: "Consultas",
                column: "ConvenioMedicoId",
                principalTable: "ConveniosMedicos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_ConveniosMedicos_ConvenioMedicoId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_ConvenioMedicoId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "ConvenioMedicoId",
                table: "Consultas");
        }
    }
}
