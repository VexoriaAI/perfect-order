using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulador.App.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerCode = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Uf = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceAverages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyBilling = table.Column<string>(type: "text", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    AvgUnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceAverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Unit = table.Column<string>(type: "text", nullable: false),
                    WeightKg = table.Column<decimal>(type: "numeric", nullable: false),
                    VolumeM3 = table.Column<decimal>(type: "numeric", nullable: false),
                    PalletUnitsDefault = table.Column<int>(type: "integer", nullable: false),
                    PackagingType = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulationRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyBilling = table.Column<string>(type: "text", nullable: false),
                    IsSeller = table.Column<bool>(type: "boolean", nullable: false),
                    IsPalletized = table.Column<bool>(type: "boolean", nullable: false),
                    ShipmentType = table.Column<string>(type: "text", nullable: false),
                    LossPercentApplied = table.Column<decimal>(type: "numeric", nullable: false),
                    FreightIdealPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    FreightAcceptablePercent = table.Column<decimal>(type: "numeric", nullable: false),
                    RequestJson = table.Column<string>(type: "text", nullable: false),
                    ResultJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkuRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    IsValidForSale = table.Column<bool>(type: "boolean", nullable: false),
                    IsInactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkuRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    DefaultLossPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    FreightIdealPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    FreightAcceptablePercent = table.Column<decimal>(type: "numeric", nullable: false),
                    MissingPricePolicy = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MaxVolumeM3 = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxWeightKg = table.Column<decimal>(type: "numeric", nullable: false),
                    MaxPallets = table.Column<int>(type: "integer", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAllowedVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAllowedVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAllowedVehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllowsPalletized = table.Column<bool>(type: "boolean", nullable: false),
                    AllowsBulk = table.Column<bool>(type: "boolean", nullable: false),
                    AllowsMix = table.Column<bool>(type: "boolean", nullable: false),
                    MaxKgPerPallet = table.Column<decimal>(type: "numeric", nullable: true),
                    SkuPerPalletPolicy = table.Column<string>(type: "text", nullable: true),
                    PalletType = table.Column<string>(type: "text", nullable: true),
                    PalletMaxHeight = table.Column<decimal>(type: "numeric", nullable: true),
                    SchedulingFee = table.Column<decimal>(type: "numeric", nullable: true),
                    NoShowFee = table.Column<decimal>(type: "numeric", nullable: true),
                    PalletFee = table.Column<decimal>(type: "numeric", nullable: true),
                    PalletRedoFee = table.Column<decimal>(type: "numeric", nullable: true),
                    StretchFee = table.Column<decimal>(type: "numeric", nullable: true),
                    BulkPerTonFee = table.Column<decimal>(type: "numeric", nullable: true),
                    BulkPerM3Fee = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerRules_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationRunId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPriceInput = table.Column<decimal>(type: "numeric", nullable: true),
                    PalletUnitsOverride = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationItems_SimulationRuns_SimulationRunId",
                        column: x => x.SimulationRunId,
                        principalTable: "SimulationRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAllowedVehicles_CustomerId_VehicleName",
                table: "CustomerAllowedVehicles",
                columns: new[] { "CustomerId", "VehicleName" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRules_CustomerId",
                table: "CustomerRules",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerCode",
                table: "Customers",
                column: "CustomerCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceAverages_CompanyBilling_Sku",
                table: "PriceAverages",
                columns: new[] { "CompanyBilling", "Sku" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Sku",
                table: "Products",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulationItems_SimulationRunId",
                table: "SimulationItems",
                column: "SimulationRunId");

            migrationBuilder.CreateIndex(
                name: "IX_SkuRules_Sku",
                table: "SkuRules",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemConfigs_Key",
                table: "SystemConfigs",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Name",
                table: "Vehicles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAllowedVehicles");

            migrationBuilder.DropTable(
                name: "CustomerRules");

            migrationBuilder.DropTable(
                name: "PriceAverages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SimulationItems");

            migrationBuilder.DropTable(
                name: "SkuRules");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SimulationRuns");
        }
    }
}
