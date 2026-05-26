using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reserva.Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoClasseUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Laboratorios_LaboratoriosId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_LaboratoriosId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "LaboratoriosId",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaboratoriosId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_LaboratoriosId",
                table: "Usuarios",
                column: "LaboratoriosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Laboratorios_LaboratoriosId",
                table: "Usuarios",
                column: "LaboratoriosId",
                principalTable: "Laboratorios",
                principalColumn: "Id");
        }
    }
}
