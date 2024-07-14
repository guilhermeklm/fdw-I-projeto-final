using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_eventos.Migrations
{
    /// <inheritdoc />
    public partial class patrocinadornaoobrigatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Patrocinador_PatrocinadorId",
                table: "Evento");

            migrationBuilder.AlterColumn<int>(
                name: "PatrocinadorId",
                table: "Evento",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "PatrocinadorId",
                table: "Evento",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Patrocinador_PatrocinadorId",
                table: "Evento",
                column: "PatrocinadorId",
                principalTable: "Patrocinador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
