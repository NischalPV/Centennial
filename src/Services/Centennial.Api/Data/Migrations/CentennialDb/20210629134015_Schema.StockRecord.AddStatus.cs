using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    public partial class SchemaStockRecordAddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "StockRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockRecords_StatusId",
                table: "StockRecords",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRecords_Statuses_StatusId",
                table: "StockRecords",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockRecords_Statuses_StatusId",
                table: "StockRecords");

            migrationBuilder.DropIndex(
                name: "IX_StockRecords_StatusId",
                table: "StockRecords");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "StockRecords");
        }
    }
}
