using System;
using System.Collections.Generic;
using Centennial.Identity.Models;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Centennial.Identity.Data
{
    public class MasterData
    {



        /// <summary>
        /// This method is to be used in ApplicationDbContext, gets invoked in OnModelCreating override.
        /// Generates migration and keeps track of changes in master data
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedUsingMigration(ModelBuilder modelBuilder)
        {
            DefaultUsers(modelBuilder);
        }

        /// <summary>
        /// Creates and saves default users
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void DefaultUsers(ModelBuilder modelBuilder)
        {
            IPasswordHasher<ApplicationUser> _passwordHasher = new PasswordHasher<ApplicationUser>();

            List<ApplicationUser> userData = new List<ApplicationUser>();

            userData.Add(new ApplicationUser(createdDate: new DateTime(2021, 6, 25, 0, 0, 0, DateTimeKind.Utc))
            {
                FirstName = "Sysadmin",
                LastName = "@Centennial",
                DateOfBirth = new DateTime(2021, 1, 1),
                Email = "admin.centennial@hotmail.com",
                UserName = "+918469940719",
                NormalizedEmail = "admin.centennial@hotmail.com".ToUpper(),
                NormalizedUserName = "+918469940719".ToUpper(),
                PhoneNumber = "+918469940719",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PasswordHash = "centennial@2021", //This is actual password. Do NOT use it directly. We need to has it before saving to database.
                SecurityStamp = "9e84634b-39ca-4d5f-9c78-c41810ea89a0",
                ConcurrencyStamp = "4f9316bb-fbd4-4e80-920f-44e5d065101e",
                Id = "c69e806e-d911-4500-af30-1fbf2409d189"
            });

            foreach (var user in userData)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);
                modelBuilder.Entity<ApplicationUser>().HasData(user);
            }
        }

        /// <summary>
        /// This method is to be used in ConfigurationDbContext, gets invoked in OnModelCreating override.
        /// Generates migration and keeps track of changes in Identity Server 4 Configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        ///
        public static void SeedIdentityServerClients(ModelBuilder modelBuilder)
        {

            var apiResources = Configuration.IdentityServer.GetApiResources();
            foreach (var apiResource in apiResources)
            {
                modelBuilder.Entity<ApiResource>().HasData(apiResource);
            }


            var apiScopes = Configuration.IdentityServer.GetApiScopes();
            foreach (var apiScope in apiScopes)
            {
                modelBuilder.Entity<ApiScope>().HasData(apiScope);
            }

            var resourceScopes = Configuration.IdentityServer.GetApiResourceScopes();
            foreach (var resourceScope in resourceScopes)
            {
                modelBuilder.Entity<ApiResourceScope>().HasData(resourceScope);
            }

            var identityResources = Configuration.IdentityServer.GetIdentityResources();
            foreach (var identityResource in identityResources)
            {
                modelBuilder.Entity<IdentityResource>().HasData(identityResource);
            }

            var identityResourceClaims = Configuration.IdentityServer.GetIdentityResourceClaims();
            foreach (var identityResourceClaim in identityResourceClaims)
            {
                modelBuilder.Entity<IdentityResourceClaim>().HasData(identityResourceClaim);
            }


            var clients = Configuration.IdentityServer.GetClients();
            foreach (var client in clients)
            {
                modelBuilder.Entity<Client>().HasData(client);
            }


            var clientCorsOrigins = Configuration.IdentityServer.GetClientCorsOrigins();
            foreach (var clientCorsOrigin in clientCorsOrigins)
            {
                modelBuilder.Entity<ClientCorsOrigin>().HasData(clientCorsOrigin);
            }


            var clientScopes = Configuration.IdentityServer.GetClientScopes();
            foreach (var scope in clientScopes)
            {
                modelBuilder.Entity<ClientScope>().HasData(scope);
            }


            var clientGrantTypes = Configuration.IdentityServer.GetClientGrantTypes();
            foreach (var grantType in clientGrantTypes)
            {
                modelBuilder.Entity<ClientGrantType>().HasData(grantType);
            }


            var clientPostLogoutRedirectUris = Configuration.IdentityServer.GetClientPostLogoutRedirectUris();
            foreach (var redirectUri in clientPostLogoutRedirectUris)
            {
                modelBuilder.Entity<ClientPostLogoutRedirectUri>().HasData(redirectUri);
            }

            var clientRedirectUris = Configuration.IdentityServer.GetClientRedirectUris();
            foreach (var uri in clientRedirectUris)
            {
                modelBuilder.Entity<ClientRedirectUri>().HasData(uri);
            }
        }


    }
}
