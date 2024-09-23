using Common;
using Common.Models;
using DataLayer;
using DataLayer.Models;
using Newtonsoft.Json;

namespace AlphaVantageTickerHarvester
{
    public static class Options
    {
        public static async Task MovingAverage1050200Day(string ticker)
        {
            string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&outputsize=full&symbol={0}&apikey={1}", ticker, Constants.ApiKeys.AlphaVantage);
            Uri queryUri = new Uri(QUERY_URL);

            HttpClient client = HttpClientSingleton.Instance;
            HttpResponseMessage response = client.GetAsync(queryUri).GetAwaiter().GetResult();
            string data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TimeSeriesDaily parsedData = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);

            List<string> days10 = parsedData.TimeSeries.Take(10).ToList().Select(x => x.Value.Close).ToList();
            double days10MovingAverage = MovingAverages.SimpleMovingAverage(days10);
            Console.WriteLine(String.Format("The 10 Day SMA for {0} is: {1}", ticker, days10MovingAverage));

            List<string> days50 = parsedData.TimeSeries.Take(50).ToList().Select(x => x.Value.Close).ToList();
            double days50MovingAverage = MovingAverages.SimpleMovingAverage(days50);
            Console.WriteLine(String.Format("The 50 Day SMA for {0} is: {1}", ticker, days50MovingAverage));

            List<string> days200 = parsedData.TimeSeries.Take(200).ToList().Select(x => x.Value.Close).ToList();
            double days200MovingAverage = MovingAverages.SimpleMovingAverage(days200);
            Console.WriteLine(String.Format("The 200 Day SMA for {0}, is : {1}", ticker, days200MovingAverage));
        }

        public static async Task PopulateDataWithTimeSeriesData(string ticker)
        {
            string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&outputsize=full&symbol={0}&apikey={1}", ticker, Constants.ApiKeys.AlphaVantage);
            Uri queryUri = new Uri(QUERY_URL);

            HttpClient client = HttpClientSingleton.Instance;
            HttpResponseMessage response = await client.GetAsync(queryUri);
            string data = await response.Content.ReadAsStringAsync();
            TimeSeriesDaily parsedData = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
            await Options.StoreTimeSeriesData(ticker, parsedData);
        }

        private static async Task StoreTimeSeriesData(string ticker, TimeSeriesDaily parsedData)
        {
            List<Task> dbCalls = new();
            TimeSeriesDailyRepository repo = new TimeSeriesDailyRepository();
            foreach (KeyValuePair<string, TimeSeriesData> kvp in parsedData.TimeSeries)
            {
                dbCalls.Add(repo.InsertTimeSeriesDaily(ticker, kvp.Key, kvp.Value));
            }
            await Task.WhenAll(dbCalls);
        }

        public static async Task PopulatePortfolioTickersWithTimeSeriesData()
        {
            List<TickerEntity> portfolioTickers = await new TickerRepository().GetPortfolioTickers();
            List<Task> timeSeriesDataStore = new();
            foreach(TickerEntity ticker in portfolioTickers)
            {
                string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&outputsize=full&symbol={0}&apikey={1}", ticker.Ticker, Constants.ApiKeys.AlphaVantage);
                Uri queryUri = new Uri(QUERY_URL);
                HttpClient client = HttpClientSingleton.Instance;
                HttpResponseMessage response = await client.GetAsync(queryUri);
                string data = await response.Content.ReadAsStringAsync();
                TimeSeriesDaily parsedData = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
                timeSeriesDataStore.Add(Options.StoreTimeSeriesData(ticker.Ticker, parsedData));
            }
            await Task.WhenAll(timeSeriesDataStore);
        }
        public static async Task PopulateIncomeStatementDataForTicker(string ticker)
        {
            string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=INCOME_STATEMENT&symbol={0}&apikey={1}", ticker, Constants.ApiKeys.AlphaVantage);
            Uri queryUri = new Uri(QUERY_URL);
            HttpClient client = HttpClientSingleton.Instance;
            HttpResponseMessage response = await client.GetAsync(queryUri);
            string data = await response.Content.ReadAsStringAsync();
            IncomeStatement parsedData = JsonConvert.DeserializeObject<IncomeStatement>(data);
            await Options.StoreAnnaulReports(ticker, parsedData);
        }

        private static async Task StoreAnnaulReports(string ticker, IncomeStatement parsedData)
        {
            List<Task> dbCalls = new();
            IncomeStatementRepository repo = new IncomeStatementRepository();
            foreach (AnnualReport report in parsedData.AnnualReports)
            {
                dbCalls.Add(repo.InsertIncomeStatement(ticker, report));
            }
            await Task.WhenAll(dbCalls);
        }
    }
}