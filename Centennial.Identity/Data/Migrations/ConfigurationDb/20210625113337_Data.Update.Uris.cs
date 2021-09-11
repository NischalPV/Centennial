using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Identity.Data.Migrations.ConfigurationDb
{
    public partial class DataUpdateUris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "http://localhost:6003");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostLogoutRedirectUri",
                value: "http://localhost:6003/");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "PostLogoutRedirectUri",
                value: "http://localhost:6002/swagger/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "RedirectUri",
                value: "http://localhost:6003/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "RedirectUri",
                value: "http://localhost:6002/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClientUri",
                value: "http://localhost:6003");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Origin",
                value: "https://localhost:6003");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostLogoutRedirectUri",
                value: "https://localhost:6003/");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "PostLogoutRedirectUri",
                value: "https://localhost:6002/swagger/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 1,
                column: "RedirectUri",
                value: "https://localhost:6003/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 2,
                column: "RedirectUri",
                value: "https://localhost:6002/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClientUri",
                value: "https://localhost:6003");
        }
    }
}
