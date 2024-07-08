using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_eventos.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Participante");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Participante",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
