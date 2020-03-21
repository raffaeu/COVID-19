using System;
using Microsoft.Extensions.Options;

namespace Covid.Data.Models
{
    public class Status : BaseItem
    {
        public DateTime ImportDate { get; set; }
        public string ConfirmedSource { get; set; }
        public string DeadSource { get; set; }
        public string RecoveredSource { get; set; }
        public int ImportedConfirmed { get; set; }
        public int ImportedDead { get; set; }
        public int ImportedRecovered { get; set; }

        public static Status Build(IOptions<ConfigurationItem> options, int importedConfirmed, int importedDead, int importedRecovered)
        {
            return new Status
            {
                Id = Guid.NewGuid(),
                ImportDate = DateTime.UtcNow,
                ImportedConfirmed = importedConfirmed,
                ImportedDead = importedDead,
                ImportedRecovered = importedRecovered,
                ConfirmedSource = options.Value.ConfirmedSource,
                DeadSource = options.Value.DeadSource,
                RecoveredSource = options.Value.RecoveredSource
            };
        }
    }
}