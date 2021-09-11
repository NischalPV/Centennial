using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    public partial class SchemaDataMaterials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Name" },
                values: new object[] { "410b785b-5484-48f5-8555-7974fe5b63dd", new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(7840), true, "SS" });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "CreatedDate", "IsActive", "Name" },
                values: new object[] { "b0217cf5-af55-4472-bf79-27167df5ee52", new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(9080), true, "Titanium" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Products");
        }
    }
}
