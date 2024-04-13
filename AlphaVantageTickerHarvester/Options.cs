using Common;
using Common.Models;
using DataLayer;
using Newtonsoft.Json;

namespace AlphaVantageTickerHarvester
{
    public static class Options
    {
        public static async Task MovingAverage1050200Day(string ticker)
        {
            string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&outputsize=full&symbol={0}&apikey={1}", ticker, Constants.ApiKeys.AlphaVantage);
            Uri queryUri = new Uri(QUERY_URL);

            using HttpClient client = HttpClientSingleton.Instance;
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

            using HttpClient client = HttpClientSingleton.Instance;
            HttpResponseMessage response = client.GetAsync(queryUri).GetAwaiter().GetResult();
            string data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            TimeSeriesDaily parsedData = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
            await Options.StoreMovingAverageTimeSeriesData(ticker, parsedData);
        }

        private static async Task StoreMovingAverageTimeSeriesData(string ticker, TimeSeriesDaily parsedData)
        {
            List<Task> dbCalls = new();
            foreach (KeyValuePair<string, TimeSeriesData> kvp in parsedData.TimeSeries)
            {
                dbCalls.Add(TimeSeriesDailyRepository.InsertTimeSeriesDaily(ticker, kvp.Key, kvp.Value));
            }
            await Task.WhenAll(dbCalls);
        }
    }
}