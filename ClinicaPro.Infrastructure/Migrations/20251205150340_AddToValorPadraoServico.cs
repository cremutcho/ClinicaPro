using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddToValorPadraoServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorPadrao",
                table: "Servicos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorPadrao",
                table: "Servicos");
        }
    }
}
