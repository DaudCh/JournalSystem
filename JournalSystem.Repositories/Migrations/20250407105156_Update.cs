using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JournalSystem.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Currency_CurrencyId1",
                table: "JournalEntries");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntries_CurrencyId1",
                table: "JournalEntries");

            migrationBuilder.DropColumn(
                name: "LineNumber",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "CurrencyId1",
                table: "JournalEntries");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "JournalLines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalLines_CurrencyId",
                table: "JournalLines",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalLines_Currency_CurrencyId",
                table: "JournalLines",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalLines_Currency_CurrencyId",
                table: "JournalLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalLines_CurrencyId",
                table: "JournalLines");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "JournalLines");

            migrationBuilder.AddColumn<int>(
                name: "LineNumber",
                table: "JournalLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId1",
                table: "JournalEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntries_CurrencyId1",
                table: "JournalEntries",
                column: "CurrencyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Currency_CurrencyId1",
                table: "JournalEntries",
                column: "CurrencyId1",
                principalTable: "Currency",
                principalColumn: "Id");
        }
    }
}
