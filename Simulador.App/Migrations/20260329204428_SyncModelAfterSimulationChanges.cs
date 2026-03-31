using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulador.App.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelAfterSimulationChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "SimulationRuns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionalId",
                table: "SimulationRuns",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "SimulationRuns",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StateCode",
                table: "SimulationRuns",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SimulationItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalCode",
                table: "SimulationItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InternalCode",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityName",
                table: "SimulationRuns");

            migrationBuilder.DropColumn(
                name: "RegionalId",
                table: "SimulationRuns");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "SimulationRuns");

            migrationBuilder.DropColumn(
                name: "StateCode",
                table: "SimulationRuns");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SimulationItems");

            migrationBuilder.DropColumn(
                name: "InternalCode",
                table: "SimulationItems");

            migrationBuilder.DropColumn(
                name: "InternalCode",
                table: "Products");
        }
    }
}
