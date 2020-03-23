using System;
using Microsoft.Extensions.Options;

namespace Covid.Data.Models
{
    public class Status : BaseItem
    {
        public DateTime ImportDate { get; set; }

        public int Records { get; set; }

        public static Status Build(int count)
        {
            return new Status
            {
                Id = Guid.NewGuid(),
                ImportDate = DateTime.Now,
                Records = count
            };
        }
    }
}