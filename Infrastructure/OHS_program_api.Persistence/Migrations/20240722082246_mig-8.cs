using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personnels_Units_UnitId",
                table: "Personnels");

            migrationBuilder.DropIndex(
                name: "IX_Personnels_UnitId",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Personnels");

            migrationBuilder.AlterColumn<string>(
                name: "TKIId",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RetiredId",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InsuranceId",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Personnels",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Personnels");

            migrationBuilder.AlterColumn<int>(
                name: "TKIId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RetiredId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InsuranceId",
                table: "Personnels",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "Personnels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personnels_UnitId",
                table: "Personnels",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnels_Units_UnitId",
                table: "Personnels",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");
        }
    }
}
