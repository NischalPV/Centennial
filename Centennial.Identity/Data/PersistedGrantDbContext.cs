using System;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Centennial.Identity.Data
{
    public class PersistedGrantDbContext : IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext<PersistedGrantDbContext>
    {
        private readonly OperationalStoreOptions _storeOptions;
        public PersistedGrantDbContext(DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
            _storeOptions = storeOptions;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
