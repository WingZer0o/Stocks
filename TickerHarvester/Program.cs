// See https://aka.ms/new-console-template for more information
using Common;
using Newtonsoft.Json;
using TickerHarvester;

string QUERY_URL = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=demo";
Uri queryUri = new Uri(QUERY_URL);

using HttpClient client = HttpClientSingleton.Instance;
HttpResponseMessage response = client.GetAsync(queryUri).GetAwaiter().GetResult();
string data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
RootObject object2 = JsonConvert.DeserializeObject<RootObject>(data);
Console.ReadLine();