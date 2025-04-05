using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalSystem.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class entityFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalLines_Account_AccountId1",
                table: "JournalLines");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalLines_CostCenter_CostCenterId1",
                table: "JournalLines");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalLines_Dimensions_DimensionId1",
                table: "JournalLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalLines_AccountId1",
                table: "JournalLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalLines_CostCenterId1",
                table: "JournalLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalLines_DimensionId1",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "CostCenterId1",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "DimensionId1",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "DimensionId",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "CostCenter");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "JournalLines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CostCenterId1",
                table: "JournalLines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DimensionId1",
                table: "JournalLines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DimensionId",
                table: "Dimensions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostCenterId",
                table: "CostCenter",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JournalLines_AccountId1",
                table: "JournalLines",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalLines_CostCenterId1",
                table: "JournalLines",
                column: "CostCenterId1");

            migrationBuilder.CreateIndex(
                name: "IX_JournalLines_DimensionId1",
                table: "JournalLines",
                column: "DimensionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalLines_Account_AccountId1",
                table: "JournalLines",
                column: "AccountId1",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalLines_CostCenter_CostCenterId1",
                table: "JournalLines",
                column: "CostCenterId1",
                principalTable: "CostCenter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalLines_Dimensions_DimensionId1",
                table: "JournalLines",
                column: "DimensionId1",
                principalTable: "Dimensions",
                principalColumn: "Id");
        }
    }
}
