using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento_eventos.Migrations
{
    /// <inheritdoc />
    public partial class createparticipanteid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Participante",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipanteId",
                table: "Inscricao",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ParticipanteUsuarioId",
                table: "Inscricao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Participante");

            migrationBuilder.DropColumn(
                name: "ParticipanteUsuarioId",
                table: "Inscricao");

            migrationBuilder.AlterColumn<string>(
                name: "ParticipanteId",
                table: "Inscricao",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inscricao_Participante_ParticipanteId",
                table: "Inscricao",
                column: "ParticipanteId",
                principalTable: "Participante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
