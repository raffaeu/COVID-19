using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Covid.Data.Models;
using CsvHelper;

namespace Covid.Etl.Services
{
    public class DataRetriever : IDataRetriever
    {
        public async Task<IEnumerable<RawData>> TransformRecordsAync(string fileSource)
        {
            var content = await DownloadFileAsync(fileSource);
            var records = new List<RawData>();
            using (var reader = new StreamReader(new MemoryStream(content)))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                string[] headerRow = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    for (var i = 0; i < 180; i++)
                    {
                        var date = DateTime.UtcNow.AddDays(-60);
                        date = date.AddDays(i);
                        object count = null;
                        if (csv.TryGetField(typeof(int), date.ToString("M/d/yy"), out count))
                        {
                            var record = new RawData
                            {
                                Id = Guid.NewGuid(),
                                State = csv.GetField(0),
                                Country = csv.GetField(1),
                                Latitude = csv.GetField(2),
                                Longitude = csv.GetField(3),
                                Date = date,
                                Count = (int)count
                            };
                            records.Add(record);
                        }
                    }
                }
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
            return null;
        }

    }
}