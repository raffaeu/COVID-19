using System;
using Covid.Data.Models;
using Covid.Data.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Covid.Etl.Startup))]

namespace Covid.Etl
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            // configuration
            builder.Services.AddOptions<ConfigurationItem>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("ConfigurationItem").Bind(settings);
                });

            // database
            string connectionString = Environment.GetEnvironmentVariable("ConfigurationItem:SqlConnection");
            builder.Services.AddDbContext<DatabaseContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        
        }
    }
}