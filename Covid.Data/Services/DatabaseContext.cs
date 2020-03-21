using Covid.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Covid.Data.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DeadCount> DeadCounts { get; set; }
        public DbSet<RecoveredCount> RecoveredCounts { get; set; }
        public DbSet<ConfirmedCount> ConfirmedCounts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeadCount>()
                .ToTable("TBL_DEAD")
                .HasKey(x => x.Id);
            modelBuilder.Entity<RecoveredCount>()
                .ToTable("TBL_RECOVERED")
                .HasKey(x => x.Id);
            modelBuilder.Entity<ConfirmedCount>()
                .ToTable("TBL_CONFIRMED")
                .HasKey(x => x.Id);

        }
    }
}