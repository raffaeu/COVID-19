using Covid.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Covid.Data.Services
{
    public class DatabaseContext : DbContext
    {

        public DbSet<RawData> RawData { get; set; }
        public DbSet<Status> Status { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawData>()
            .ToTable("TBL_RAW_DATA")
            .HasKey(x => x.Id);
            
            modelBuilder.Entity<Status>()
            .ToTable("TBL_IMPORT_STATUS")
            .HasKey(x => x.Id);

            modelBuilder.Entity<RawData>()
            .HasIndex(x => x.Country)
            .IsClustered(false)
            .HasName("IX_RAW_DATA_COUNTRY");

            modelBuilder.Entity<RawData>()
            .HasIndex(x => x.State)
            .IsClustered(false)
            .HasName("IX_RAW_DATA_STATE");

            modelBuilder.Entity<RawData>()
            .HasIndex(x => x.Date)
            .IsClustered(false)
            .HasName("IX_RAW_DATA_DATE");

        }
    }
}