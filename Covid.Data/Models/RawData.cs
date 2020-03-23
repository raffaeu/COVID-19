using System;

namespace Covid.Data.Models
{
    public class RawData : BaseItem
    {
        public string State { get; set; }
        public string Country { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Date { get; set; }
        public int ConfirmedCount { get; set; }
        public int RecoveredCount { get; set; }
        public int DeadCount { get; set; }
    }
}