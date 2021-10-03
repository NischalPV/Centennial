using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Centennial.Identity.Data.Migrations.ConfigurationDb
{
    public partial class DataAddIdentityClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AbsoluteRefreshTokenLifetime", "AccessTokenLifetime", "AccessTokenType", "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AllowPlainTextPkce", "AllowRememberConsent", "AllowedIdentityTokenSigningAlgorithms", "AlwaysIncludeUserClaimsInIdToken", "AlwaysSendClientClaims", "AuthorizationCodeLifetime", "BackChannelLogoutSessionRequired", "BackChannelLogoutUri", "ClientClaimsPrefix", "ClientId", "ClientName", "ClientUri", "ConsentLifetime", "Created", "Description", "DeviceCodeLifetime", "EnableLocalLogin", "Enabled", "FrontChannelLogoutSessionRequired", "FrontChannelLogoutUri", "IdentityTokenLifetime", "IncludeJwtId", "LastAccessed", "LogoUri", "NonEditable", "PairWiseSubjectSalt", "ProtocolType", "RefreshTokenExpiration", "RefreshTokenUsage", "RequireClientSecret", "RequireConsent", "RequirePkce", "RequireRequestObject", "SlidingRefreshTokenLifetime", "UpdateAccessTokenClaimsOnRefresh", "Updated", "UserCodeType", "UserSsoLifetime" },
                values: new object[] { 3, 2592000, 3600, 0, true, false, false, true, null, false, false, 300, true, null, "client_", "centennial-angular--prod", "Centennial Angular Frontend OpenId Client", "http://219.91.186.71:6003", null, new DateTime(2021, 6, 25, 11, 7, 29, 917, DateTimeKind.Utc).AddTicks(7870), "Production", 300, true, true, true, null, 300, true, null, null, false, null, "oidc", 1, 1, true, false, true, false, 1296000, false, null, null, null });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AbsoluteRefreshTokenLifetime", "AccessTokenLifetime", "AccessTokenType", "AllowAccessTokensViaBrowser", "AllowOfflineAccess", "AllowPlainTextPkce", "AllowRememberConsent", "AllowedIdentityTokenSigningAlgorithms", "AlwaysIncludeUserClaimsInIdToken", "AlwaysSendClientClaims", "AuthorizationCodeLifetime", "BackChannelLogoutSessionRequired", "BackChannelLogoutUri", "ClientClaimsPrefix", "ClientId", "ClientName", "ClientUri", "ConsentLifetime", "Created", "Description", "DeviceCodeLifetime", "EnableLocalLogin", "Enabled", "FrontChannelLogoutSessionRequired", "FrontChannelLogoutUri", "IdentityTokenLifetime", "IncludeJwtId", "LastAccessed", "LogoUri", "NonEditable", "PairWiseSubjectSalt", "ProtocolType", "RefreshTokenExpiration", "RefreshTokenUsage", "RequireClientSecret", "RequireConsent", "RequirePkce", "RequireRequestObject", "SlidingRefreshTokenLifetime", "UpdateAccessTokenClaimsOnRefresh", "Updated", "UserCodeType", "UserSsoLifetime" },
                values: new object[] { 4, 2592000, 3600, 0, true, false, false, true, null, false, false, 300, true, null, "client_", "centennial-api--prod", "Centennail API Swagger UI", null, null, new DateTime(2021, 6, 25, 11, 7, 29, 917, DateTimeKind.Utc).AddTicks(7870), "Production", 300, true, true, true, null, 300, true, null, null, false, null, "oidc", 1, 1, true, false, true, false, 1296000, false, null, null, null });

            migrationBuilder.InsertData(
                table: "ClientCorsOrigins",
                columns: new[] { "Id", "ClientId", "Origin" },
                values: new object[] { 2, 3, "http://219.91.186.71:6003" });

            migrationBuilder.InsertData(
                table: "ClientGrantTypes",
                columns: new[] { "Id", "ClientId", "GrantType" },
                values: new object[,]
                {
                    { 3, 3, "implicit" },
                    { 4, 4, "implicit" }
                });

            migrationBuilder.InsertData(
                table: "ClientPostLogoutRedirectUris",
                columns: new[] { "Id", "ClientId", "PostLogoutRedirectUri" },
                values: new object[,]
                {
                    { 3, 3, "http://219.91.186.71:6003/" },
                    { 4, 4, "http://219.91.186.71:6002/swagger/" }
                });

            migrationBuilder.InsertData(
                table: "ClientRedirectUris",
                columns: new[] { "Id", "ClientId", "RedirectUri" },
                values: new object[,]
                {
                    { 3, 3, "http://219.91.186.71:6003/" },
                    { 4, 4, "http://219.91.186.71:6002/swagger/oauth2-redirect.html" }
                });

            migrationBuilder.InsertData(
                table: "ClientScopes",
                columns: new[] { "Id", "ClientId", "Scope" },
                values: new object[,]
                {
                    { 5, 3, "openid" },
                    { 6, 3, "profile" },
                    { 7, 3, "API" },
                    { 8, 4, "API" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientCorsOrigins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClientGrantTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClientPostLogoutRedirectUris",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClientRedirectUris",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClientScopes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
