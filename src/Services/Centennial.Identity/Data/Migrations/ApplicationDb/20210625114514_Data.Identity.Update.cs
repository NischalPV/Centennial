using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Identity.Data.Migrations.ApplicationDb
{
    public partial class DataIdentityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c69e806e-d911-4500-af30-1fbf2409d189",
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "+918469940719", "AQAAAAEAACcQAAAAEAZOd0naDIbzUclstWF8Tfax6taewz+i+4xSA45I1M77liVffrrPb0TeOyO7zrO1Bw==", "+918469940719" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c69e806e-d911-4500-af30-1fbf2409d189",
                columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
                values: new object[] { "ADMIN.CENTENNIAL@HOTMAIL.COM", "AQAAAAEAACcQAAAAEE2vF4gA0Ptx/gF0U4qNdC8pH2bD6AT3Eyl2Q6c2i90r+LpLtQBb04D4R9rxMuBfgQ==", "admin.centennial@hotmail.com" });
        }
    }
}
