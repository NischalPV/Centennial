using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Api.Data.Migrations.CentennialDb
{
    public partial class SchemaProcessAddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutBound",
                table: "Processes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutBound",
                table: "Processes");
        }
    }
}
