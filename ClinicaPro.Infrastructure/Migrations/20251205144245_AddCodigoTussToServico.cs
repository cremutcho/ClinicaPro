using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCodigoTussToServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoTuss",
                table: "Servicos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoTuss",
                table: "Servicos");
        }
    }
}
