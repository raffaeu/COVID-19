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
        public int Count { get; set; }
    }
}