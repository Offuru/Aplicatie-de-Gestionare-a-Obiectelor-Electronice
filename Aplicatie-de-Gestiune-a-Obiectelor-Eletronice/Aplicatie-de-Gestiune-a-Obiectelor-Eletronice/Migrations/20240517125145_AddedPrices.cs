using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "ElectronicObjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "ElectronicObjects");
        }
    }
}
