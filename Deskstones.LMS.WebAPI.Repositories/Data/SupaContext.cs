namespace Deskstones.LMS.WebAPI.Repositories.Data
{
    using Microsoft.EntityFrameworkCore;
    public sealed class SupaContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public SupaContext(DbContextOptions<SupaContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Optional: Configure entity properties or relationships
        }
    }
}
