using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reserva.Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoClasseReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Laboratorios_LaboratoriosId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_LaboratoriosId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "LaboratoriosId",
                table: "Reservas");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_LaboratorioId",
                table: "Reservas",
                column: "LaboratorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Laboratorios_LaboratorioId",
                table: "Reservas",
                column: "LaboratorioId",
                principalTable: "Laboratorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Laboratorios_LaboratorioId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_LaboratorioId",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoriosId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_LaboratoriosId",
                table: "Reservas",
                column: "LaboratoriosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Laboratorios_LaboratoriosId",
                table: "Reservas",
                column: "LaboratoriosId",
                principalTable: "Laboratorios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
