using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Migrations
{
    /// <inheritdoc />
    public partial class adas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectronicObject",
                table: "ElectronicObject");

            migrationBuilder.RenameTable(
                name: "ElectronicObject",
                newName: "ElectronicObjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectronicObjects",
                table: "ElectronicObjects",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectronicObjects",
                table: "ElectronicObjects");

            migrationBuilder.RenameTable(
                name: "ElectronicObjects",
                newName: "ElectronicObject");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectronicObject",
                table: "ElectronicObject",
                column: "Id");
        }
    }
}
