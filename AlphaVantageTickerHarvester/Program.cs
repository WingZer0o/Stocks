// See https://aka.ms/new-console-template for more information
using Common;
using DataLayer;
using Newtonsoft.Json;
using TickerHarvester;

Console.Write("Enter Ticker To Populate Local SQL Server: ");
string userTickerInput = Console.ReadLine();
TickerRepository.InsertTicker(userTickerInput);

string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey={1}", userTickerInput, Constants.ApiKeys.AlphaVantage);
Uri queryUri = new Uri(QUERY_URL);

using HttpClient client = HttpClientSingleton.Instance;
HttpResponseMessage response = client.GetAsync(queryUri).GetAwaiter().GetResult();
string data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
TimeSeriesDaily object2 = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
// populate the time series data for the ticker.
Console.ReadLine();