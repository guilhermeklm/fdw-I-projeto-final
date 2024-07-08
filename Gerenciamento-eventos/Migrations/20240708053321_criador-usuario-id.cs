using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_eventos.Migrations
{
    /// <inheritdoc />
    public partial class criadorusuarioid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Criador_CriadorId",
                table: "Evento");

            migrationBuilder.AlterColumn<string>(
                name: "CriadorId",
                table: "Evento",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CriadorUsuarioId",
                table: "Evento",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Criador_CriadorId",
                table: "Evento",
                column: "CriadorId",
                principalTable: "Criador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Criador_CriadorId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "CriadorUsuarioId",
                table: "Evento");

            migrationBuilder.AlterColumn<string>(
                name: "CriadorId",
                table: "Evento",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Criador_CriadorId",
                table: "Evento",
                column: "CriadorId",
                principalTable: "Criador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
