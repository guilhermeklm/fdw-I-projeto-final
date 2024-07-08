using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_eventos.Migrations
{
    /// <inheritdoc />
    public partial class removerelatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatrocinadorId",
                table: "Evento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Patrocinador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrocinador", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_PatrocinadorId",
                table: "Evento",
                column: "PatrocinadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Patrocinador_PatrocinadorId",
                table: "Evento",
                column: "PatrocinadorId",
                principalTable: "Patrocinador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Patrocinador_PatrocinadorId",
                table: "Evento");

            migrationBuilder.DropTable(
                name: "Patrocinador");

            migrationBuilder.DropIndex(
                name: "IX_Evento_PatrocinadorId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "PatrocinadorId",
                table: "Evento");
        }
    }
}
