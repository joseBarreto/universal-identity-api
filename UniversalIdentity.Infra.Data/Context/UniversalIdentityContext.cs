using Microsoft.EntityFrameworkCore;
using UniversalIdentity.Domain.Entities;

namespace UniversalIdentity.Infra.Data.Context
{
    public class UniversalIdentityContext : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Login> Login { get; set; }

        public UniversalIdentityContext(DbContextOptions<UniversalIdentityContext> options = null) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Method intentionally left empty.
        }
    }
}
