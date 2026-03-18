using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulador.App.Migrations
{
    /// <inheritdoc />
    public partial class AddBillingCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCompanies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingCompanies_CompanyCode",
                table: "BillingCompanies",
                column: "CompanyCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingCompanies");
        }
    }
}
