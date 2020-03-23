using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Covid.Data.Models;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Covid.Etl.Services
{
    public class DataRetriever : IDataRetriever
    {
        private readonly IOptions<ConfigurationItem> options;
        private readonly ILogger<DataRetriever> logger;

        public DataRetriever(IOptions<ConfigurationItem> options, ILogger<DataRetriever> logger)
        {
            this.options = options;
            this.logger = logger;
        }
        public async Task<IEnumerable<RawData>> TransformRecordsAync(DateTime date)
        {
            var filename = string.Format(options.Value.DailyReport, date.ToString("MM-dd-yyyy"));
            var records = new List<RawData>();
            try 
            {
                var content = await DownloadFileAsync(filename);
                using (var reader = new StreamReader(new MemoryStream(content)))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    string[] headerRow = csv.Context.HeaderRecord;

                    while (csv.Read())
                    {
                        var record = new RawData
                        {
                            Id = Guid.NewGuid(),
                            State = csv.GetField(0),
                            Country = csv.GetField(1),
                            Date = date,
                            ConfirmedCount = Convert.ToInt32(csv.GetField(3)),
                            DeadCount = Convert.ToInt32(csv.GetField(4)),
                            RecoveredCount = Convert.ToInt32(csv.GetField(5))
                        };
                        records.Add(record);

                    }
                }
            } 
            catch(Exception ex)
            {
                logger.LogError(ex, $"File not found for {date}");
            }

            return records;
        }

        public async Task<byte[]> DownloadFileAsync(string url)
        {
            using (var client = new HttpClient())
            {

                using (var result = await client.GetAsync(url))
                {
                    if (result.IsSuccessStatusCode)
                    {
                        return await result.Content.ReadAsByteArrayAsync();
                    }

                }
            }
            throw new Exception("File not found or corrupted");
        }

    }
}