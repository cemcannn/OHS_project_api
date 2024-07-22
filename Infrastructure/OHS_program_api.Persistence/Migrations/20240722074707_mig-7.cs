using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OHSprogramapi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Accidents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
