using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    public partial class SchemaEmployeeRawMaterialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RawMaterials",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RawMaterials");
        }
    }
}
