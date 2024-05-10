using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Migrations
{
    /// <inheritdoc />
    public partial class migrare1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Order",
                table: "ElectronicObject",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ElectronicObject",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
