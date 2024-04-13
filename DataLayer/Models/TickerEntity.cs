namespace DataLayer.Models
{
    public class TickerEntity
    {
        public Guid ID { get; set; }
        public string Ticker { get; set; }
        public bool IsInPortfolio { get; set; }
    }
}
