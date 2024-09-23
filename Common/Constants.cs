namespace Common
{
    public class Constants
    {
        public static class ApiKeys
        {
            public static string AlphaVantage = Environment.GetEnvironmentVariable("AlphaVantageApiKey");
        }

        public class ConnectionStrings
        {
            public const string StocksDatabase = "Data Source=MM-Desktop;Initial Catalog=Stocks;Integrated Security=True;Encrypt=False";
        }
    }
}
