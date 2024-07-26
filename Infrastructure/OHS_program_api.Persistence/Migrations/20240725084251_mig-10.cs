using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnTheJobDate",
                table: "Accidents");

            migrationBuilder.RenameColumn(
                name: "StartDateOfWork",
                table: "Personnels",
                newName: "BornDate");

            migrationBuilder.AddColumn<string>(
                name: "ReportDay",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportDay",
                table: "Accidents");

            migrationBuilder.RenameColumn(
                name: "BornDate",
                table: "Personnels",
                newName: "StartDateOfWork");

            migrationBuilder.AddColumn<DateTime>(
                name: "OnTheJobDate",
                table: "Accidents",
                type: "datetime2",
                nullable: true);
        }
    }
}
