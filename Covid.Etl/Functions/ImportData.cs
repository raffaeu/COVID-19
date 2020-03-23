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
using System;

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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var date = new DateTime(2020, 01, 21);
            var count = 0;
            if (repository.Query<RawData>().Any())
            {
                date = repository.Query<RawData>().Max(x => x.Date);
            } 

            var daysDiff = (DateTime.UtcNow - date).TotalDays;

            for(var i = 1; i < daysDiff; i++)
            {
                log.LogInformation($"Importing data for {date.AddDays(i)}");
                var rawData = await dataRetriever.TransformRecordsAync(date.AddDays(i));
                await repository.BulkInsert(rawData.ToList());
                count += rawData.Count();
            }

            var status = Status.Build(count);

            status = await repository.Insert(status);

            return new OkObjectResult(status);
        }
    }
}
