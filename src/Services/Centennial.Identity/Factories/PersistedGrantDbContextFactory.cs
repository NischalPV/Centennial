using System;
using System.IO;
using Centennial.Identity.Data;
//using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Centennial.Identity.Factories
{
    public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            var operationOptions = new OperationalStoreOptions();

            optionsBuilder.UseSqlServer(config["ConnectionStrings:DefaultConnection"], sqlServerOptionsAction: o => o.MigrationsAssembly("Centennial.Identity"));

            return new PersistedGrantDbContext(optionsBuilder.Options, operationOptions);
        }
    }
}
