﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LimbId",
                table: "Accidents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_LimbId",
                table: "Accidents",
                column: "LimbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accidents_Limbs_LimbId",
                table: "Accidents",
                column: "LimbId",
                principalTable: "Limbs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accidents_Limbs_LimbId",
                table: "Accidents");

            migrationBuilder.DropIndex(
                name: "IX_Accidents_LimbId",
                table: "Accidents");

            migrationBuilder.DropColumn(
                name: "LimbId",
                table: "Accidents");
        }
    }
}
