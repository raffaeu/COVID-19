using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Covid.Data.Services;

namespace Covid.Data
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=40.76.223.133;Database=Covid19Db;User Id=covid;Password=c0v1d!!!c0v1d!!!;");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}