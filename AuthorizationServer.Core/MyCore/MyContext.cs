using AuthorizationServer.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationServer.Core.MyCore
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<UserXClient> UserXClients { get; set; }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }
        public DbSet<ClientScope> ClientScopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserXClient>().ToTable("UserXClient");
            modelBuilder.Entity<ClientRedirectUri>().ToTable("ClientRedirectUris");
            modelBuilder.Entity<ClientScope>().ToTable("ClientScopes");
        }
    }
}
