using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Migrations
{
    /// <inheritdoc />
    public partial class PriceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "ElectronicObjects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ElectronicObjects");
        }
    }
}
