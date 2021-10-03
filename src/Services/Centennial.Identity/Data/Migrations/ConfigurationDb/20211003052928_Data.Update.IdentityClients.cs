using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Identity.Data.Migrations.ConfigurationDb
{
    public partial class DataUpdateIdentityClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Origin",
                value: "https://centennial-web.azurewebsites.net");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 3,
                column: "PostLogoutRedirectUri",
                value: "https://centennial-web.azurewebsites.net/");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "PostLogoutRedirectUri",
                value: "https://centennial-api.azurewebsites.net/swagger/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 3,
                column: "RedirectUri",
                value: "https://centennial-web.azurewebsites.net/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "RedirectUri",
                value: "https://centennial-api.azurewebsites.net/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClientId", "ClientUri" },
                values: new object[] { "centennial-angular--prod", "https://centennial-web.azurewebsites.net" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClientId",
                value: "centennial-api--prod");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 2,
                column: "Origin",
                value: "http://219.91.186.71:6003");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 3,
                column: "PostLogoutRedirectUri",
                value: "http://219.91.186.71:6003/");

            migrationBuilder.UpdateData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "PostLogoutRedirectUri",
                value: "http://219.91.186.71:6002/swagger/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 3,
                column: "RedirectUri",
                value: "http://219.91.186.71:6003/");

            migrationBuilder.UpdateData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4,
                column: "RedirectUri",
                value: "http://219.91.186.71:6002/swagger/oauth2-redirect.html");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClientId", "ClientUri" },
                values: new object[] { "centennial-angular", "http://219.91.186.71:6003" });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClientId",
                value: "centennial-api");
        }
    }
}
