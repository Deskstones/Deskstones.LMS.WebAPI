namespace Deskstones.LMS.Infrastructure.Data
{
    using Deskstones.LMS.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public sealed class RailwayContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserSocial> UserSocial { get; set; }

        public RailwayContext(DbContextOptions<RailwayContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → UserProfile (1-to-1)
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProfile → UserAddress (1-to-1)
            modelBuilder.Entity<UserAddress>()
                .HasOne(ua => ua.UserProfile)
                .WithOne(up => up.Address)
                .HasForeignKey<UserAddress>(ua => ua.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProfile → UserSocial (1-to-1)
            modelBuilder.Entity<UserSocial>()
                .HasOne(us => us.UserProfile)
                .WithOne()
                .HasForeignKey<UserSocial>(us => us.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
