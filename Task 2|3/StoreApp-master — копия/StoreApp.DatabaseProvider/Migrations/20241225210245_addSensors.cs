using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreApp.DatabaseProvider.Migrations
{
    public partial class addSensors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sensor");

            migrationBuilder.AddColumn<float>(
                name: "Humidity",
                table: "Sensor",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Temperature",
                table: "Sensor",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Sensor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Sensor");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Sensor");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sensor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Sensor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
