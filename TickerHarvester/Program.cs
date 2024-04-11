// See https://aka.ms/new-console-template for more information
using Common;
using Newtonsoft.Json;
using TickerHarvester;

Console.Write("Enter Ticker: ");
string userTickerInput = Console.ReadLine();

string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={0}&apikey={1}", userTickerInput, Constants.ApiKeys.AlphaVantage);
Uri queryUri = new Uri(QUERY_URL);

using HttpClient client = HttpClientSingleton.Instance;
HttpResponseMessage response = client.GetAsync(queryUri).GetAwaiter().GetResult();
string data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
TimeSeriesDaily object2 = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
Console.ReadLine();