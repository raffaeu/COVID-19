using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Covid.Etl.Services;
using Microsoft.Extensions.Options;
using Covid.Data.Models;
using System.Linq;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Covid.Etl.Functions
{
    public class ImportData
    {
        private readonly IOptions<ConfigurationItem> configuration;
        private readonly IDataRetriever dataRetriever;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public ImportData(IOptions<ConfigurationItem> configuration, IDataRetriever dataRetriever, IRepository repository, IMapper mapper)
        {
            this.configuration = configuration;
            this.dataRetriever = dataRetriever;
            this.repository = repository;
            this.mapper = mapper;
        }

        [FunctionName("ImportData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // dead count
            var deadRawData = await dataRetriever
                .TransformRecordsAync(configuration.Value.DeadSource);
            var deadRecords = mapper.ProjectTo<DeadCount>(deadRawData.AsQueryable());
            await repository.BulkInsert(deadRecords.ToList());

            // confirmed count
            var confirmedRawData = await dataRetriever
                .TransformRecordsAync(configuration.Value.ConfirmedSource);
            var confirmedRecords = mapper.ProjectTo<ConfirmedCount>(confirmedRawData.AsQueryable());
            await repository.BulkInsert(confirmedRecords.ToList());

            // recovered count
            var recoveredRawData = await dataRetriever
                .TransformRecordsAync(configuration.Value.RecoveredSource);
            var recoveredRecords = mapper.ProjectTo<RecoveredCount>(recoveredRawData.AsQueryable());
            await repository.BulkInsert(recoveredRecords.ToList());

            return new OkObjectResult(new { DeclaredType = deadRawData.Count(), Confirmed = confirmedRawData.Count(), Recovered = recoveredRawData.Count()});
        }
    }
}
