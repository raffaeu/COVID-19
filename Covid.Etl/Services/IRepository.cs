using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Covid.Data.Models;
using Covid.Data.Services;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Covid.Etl.Services
{
    public interface IRepository
    {
        Task BulkInsert<TEntity>(IList<TEntity> records) where TEntity : BaseItem;
    }

    public class Repository : IRepository
    {
        private readonly ILogger<Repository> logger;
        private readonly DatabaseContext context;

        public Repository(ILogger<Repository> logger, DatabaseContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task BulkInsert<TEntity>(IList<TEntity> records) where TEntity : BaseItem
        {
            var connection = context.Database.GetDbConnection();
            await connection.OpenAsync();
            await context.TruncateAsync<TEntity>();

            logger.LogInformation($"Starting BULK Import {records.Count}");
            await context.BulkInsertAsync(records, new BulkConfig{
                BatchSize = 6000
            }, (count) => logger.LogInformation($"Inserted {count:P2} records"), default(CancellationToken));
            logger.LogInformation($"BULK Import completed  {records.Count}");

            await connection.CloseAsync();
        }
    }
}