using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNomeToServico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Servicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Servicos");
        }
    }
}
