using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ActualDailyWages");

            migrationBuilder.AlterColumn<string>(
                name: "ActualWageUnderground",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ActualWageSurface",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Directorate",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeesNumberSurface",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeesNumberUnderground",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Directorate",
                table: "ActualDailyWages");

            migrationBuilder.DropColumn(
                name: "EmployeesNumberSurface",
                table: "ActualDailyWages");

            migrationBuilder.DropColumn(
                name: "EmployeesNumberUnderground",
                table: "ActualDailyWages");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "ActualDailyWages");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ActualDailyWages");

            migrationBuilder.AlterColumn<string>(
                name: "ActualWageUnderground",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActualWageSurface",
                table: "ActualDailyWages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ActualDailyWages",
                type: "datetime2",
                nullable: true);
        }
    }
}
