using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualDailyWages");

            migrationBuilder.RenameColumn(
                name: "ReportDay",
                table: "Accidents",
                newName: "LostDayOfWork");

            migrationBuilder.CreateTable(
                name: "AccidentStatistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Directorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualDailyWageSurface = table.Column<long>(type: "bigint", nullable: true),
                    ActualDailyWageUnderground = table.Column<long>(type: "bigint", nullable: true),
                    EmployeesNumberSurface = table.Column<int>(type: "int", nullable: true),
                    EmployeesNumberUnderground = table.Column<int>(type: "int", nullable: true),
                    AccidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccidentStatistics_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccidentStatistics_AccidentId",
                table: "AccidentStatistics",
                column: "AccidentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentStatistics");

            migrationBuilder.RenameColumn(
                name: "LostDayOfWork",
                table: "Accidents",
                newName: "ReportDay");

            migrationBuilder.CreateTable(
                name: "ActualDailyWages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActualWageSurface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualWageUnderground = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Directorate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeesNumberSurface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeesNumberUnderground = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActualDailyWages", x => x.Id);
                });
        }
    }
}
