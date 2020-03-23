using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Data.Models;

namespace Covid.Etl.Services
{
    public interface IDataRetriever
    {
        Task<byte[]> DownloadFileAsync(string url);
        Task<IEnumerable<RawData>> TransformRecordsAync(DateTime date);
    }
}