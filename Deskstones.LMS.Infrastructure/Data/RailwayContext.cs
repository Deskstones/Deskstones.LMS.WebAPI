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
        public DbSet<TeacherProfile> TeacherProfile { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Cohort> Cohort { get; set; }

        public RailwayContext(DbContextOptions<RailwayContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User ↔ UserProfile (1-to-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User ↔ TeacherProfile (1-to-1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.TeacherProfile)
                .WithOne(tp => tp.User)
                .HasForeignKey<TeacherProfile>(tp => tp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProfile ↔ UserAddress (1-to-1)
            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.Address)
                .WithOne(ua => ua.UserProfile)
                .HasForeignKey<UserAddress>(ua => ua.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserProfile ↔ UserSocial (1-to-1)
            modelBuilder.Entity<UserSocial>()
                .HasOne(us => us.UserProfile)
                .WithOne()
                .HasForeignKey<UserSocial>(us => us.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cohort ↔ TeacherProfile (many-to-one)
            modelBuilder.Entity<Cohort>()
                .HasOne(c => c.TeacherProfile)
                .WithMany(tp => tp.Cohorts)
                .HasForeignKey(c => c.TeacherProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cohort ↔ Subject (many-to-one)
            modelBuilder.Entity<Cohort>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.Cohorts)
                .HasForeignKey(c => c.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
