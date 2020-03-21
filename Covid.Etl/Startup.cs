using System;
using AutoMapper;
using Covid.Data.Models;
using Covid.Data.Services;
using Covid.Etl.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: FunctionsStartup(typeof(Covid.Etl.Startup))]

namespace Covid.Etl
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // serilog
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            builder.Services.AddLogging(lb => lb.AddSerilog(logger));

            // automapper
            builder.Services.AddAutoMapper(typeof(Startup));

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

            // services
            builder.Services.AddTransient<IDataRetriever, DataRetriever>();
            builder.Services.AddTransient<IRepository, Repository>();
        
        }
    }
}