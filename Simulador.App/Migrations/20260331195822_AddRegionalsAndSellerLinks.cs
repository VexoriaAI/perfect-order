using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulador.App.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionalsAndSellerLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "SimulationRuns",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Regionals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regionals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerRegionals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerUserId = table.Column<string>(type: "text", nullable: false),
                    RegionalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerRegionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerRegionals_AspNetUsers_SellerUserId",
                        column: x => x.SellerUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerRegionals_Regionals_RegionalId",
                        column: x => x.RegionalId,
                        principalTable: "Regionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Regionals_Code",
                table: "Regionals",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellerRegionals_RegionalId",
                table: "SellerRegionals",
                column: "RegionalId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerRegionals_SellerUserId_RegionalId",
                table: "SellerRegionals",
                columns: new[] { "SellerUserId", "RegionalId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerRegionals");

            migrationBuilder.DropTable(
                name: "Regionals");

            migrationBuilder.AlterColumn<Guid>(
                name: "SellerId",
                table: "SimulationRuns",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
