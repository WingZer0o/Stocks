﻿namespace DataLayer.Models
{
    public class TimeSeriesDailyEntity
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public string Close { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Open { get; set; }
        public string Volumne { get; set; }
        public Guid Ticker { get; set; }

    }
}
