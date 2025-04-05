using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalSystem.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DimensionID",
                table: "Dimensions",
                newName: "DimensionId");

            migrationBuilder.RenameColumn(
                name: "CostCenterID",
                table: "CostCenter",
                newName: "CostCenterId");

            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Account",
                newName: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DimensionId",
                table: "Dimensions",
                newName: "DimensionID");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                table: "CostCenter",
                newName: "CostCenterID");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Account",
                newName: "AccountID");
        }
    }
}
