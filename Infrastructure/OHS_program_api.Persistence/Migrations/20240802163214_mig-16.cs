using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccidentStatistics_Accidents_AccidentId",
                table: "AccidentStatistics");

            migrationBuilder.DropIndex(
                name: "IX_AccidentStatistics_AccidentId",
                table: "AccidentStatistics");

            migrationBuilder.DropColumn(
                name: "AccidentId",
                table: "AccidentStatistics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccidentId",
                table: "AccidentStatistics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccidentStatistics_AccidentId",
                table: "AccidentStatistics",
                column: "AccidentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccidentStatistics_Accidents_AccidentId",
                table: "AccidentStatistics",
                column: "AccidentId",
                principalTable: "Accidents",
                principalColumn: "Id");
        }
    }
}
