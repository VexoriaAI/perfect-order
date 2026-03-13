using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulador.App.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUnitPriceToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DefaultUnitPrice",
                table: "Products",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultUnitPrice",
                table: "Products");
        }
    }
}
