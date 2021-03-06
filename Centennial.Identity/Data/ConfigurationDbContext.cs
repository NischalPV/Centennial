using System;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Centennial.Identity.Data
{
    public class ConfigurationDbContext : IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext<ConfigurationDbContext>
    {
        private readonly ConfigurationStoreOptions _storeOptions;

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {
            _storeOptions = storeOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(_storeOptions);
            modelBuilder.ConfigureResourcesContext(_storeOptions);

            base.OnModelCreating(modelBuilder);

            MasterData.SeedIdentityServerClients(modelBuilder);
        }
    }
}
