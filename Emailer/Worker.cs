using Common;
using Common.Models;
using DataLayer;
using DataLayer.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace Emailer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public class EmailTemplate
        {
            public string Body { get; set; }
            public string Date { get; set; }
            public string Ticker { get; set; }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<EmailTemplate> emailTemplates = new();
            List<TickerEntity> tickers = await new TickerRepository().GetTickers();
            foreach (TickerEntity ticker in tickers)
            {
                // Get the most recent time series daily data in compact version to cut down on API data and populate today's date into our database
                string QUERY_URL = String.Format("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&outputsize=compact&symbol={0}&apikey={1}", ticker.Ticker, Constants.ApiKeys.AlphaVantage);
                Uri queryUri = new Uri(QUERY_URL);
                HttpClient client = HttpClientSingleton.Instance;
                HttpResponseMessage response = await client.GetAsync(queryUri);
                string data = await response.Content.ReadAsStringAsync();
                TimeSeriesDaily parsedData = JsonConvert.DeserializeObject<TimeSeriesDaily>(data);
                TimeSeriesDailyRepository repo = new TimeSeriesDailyRepository();
                string mostRecentDate = parsedData.TimeSeries.Keys.First();
                TimeSeriesData mostRecentData = parsedData.TimeSeries.Values.First();
                await repo.InsertTimeSeriesDaily(ticker.Ticker, parsedData.TimeSeries.Keys.First(), parsedData.TimeSeries.Values.First());

                // Construct query and construct a crappy email string for the SMAs.
                List<TimeSeriesDailyEntity> timesSeriesData = await repo.GetTimeSeriesDailyByCount(ticker.Ticker, 200);
                List<string> days10 = timesSeriesData.Take(10).ToList().Select(x => x.Close).ToList();
                double days10MovingAverage = MovingAverages.SimpleMovingAverage(days10);
                double days10Linear = MovingAverages.LinearWeightedMovingAverage(days10);

                List<string> days50 = timesSeriesData.Take(50).ToList().Select(x => x.Close).ToList();
                double days50MovingAverage = MovingAverages.SimpleMovingAverage(days50);
                double days50Linear = MovingAverages.LinearWeightedMovingAverage(days50);

                List<string> days200 = timesSeriesData.Take(200).ToList().Select(x => x.Close).ToList();
                double days200MovingAverage = MovingAverages.SimpleMovingAverage(days200);
                double days200Linear = MovingAverages.LinearWeightedMovingAverage(days200);

                string bodyHtml = String.Format(@"
                <html>
                    <body>
                        <p>The name of this Ticker is: <b>{0}</b>. The most recent closing price was ${1} on {2}</p>
                        <p>The 10, 50, 200 Day Simple Moving Average is</p>
                        <p>
                            <ul>
                                <li>10 Day: ${3}</li>
                                <li>50 Day: ${4}</li>
                                <li>200 Day: ${5}</li>
                            </ul>
                        </p>
                        <p>The 10, 50, 200 Day Linear Moving Average is</p>
                        <p>
                            <ul>
                                <li>10 Day: ${6}</li>
                                <li>50 Day: ${7}</li>
                                <li>200 Day: ${8}</li>
                            </ul>
                        </p>
                    </body>
                </html>
                ", ticker.Ticker, mostRecentData.Close, mostRecentDate, days10MovingAverage, days50MovingAverage, days200MovingAverage, days10Linear, days50Linear, days200Linear);

                emailTemplates.Add(new EmailTemplate() {
                    Body = bodyHtml,
                    Ticker = ticker.Ticker,
                    Date = mostRecentDate
                });
            }

            foreach(EmailTemplate template in emailTemplates)
            {
                using MailMessage mail = new MailMessage();
                mail.From = new MailAddress("mikemulchrone987@gmail.com");
                mail.To.Add("mikemulchrone987@gmail.com");
                mail.Subject = String.Format("{0} Times Series Daily for {1}", template.Date, template.Ticker);
                mail.Body = template.Body;
                mail.IsBodyHtml = true;
                using SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                string email = Environment.GetEnvironmentVariable("Email");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(email, Environment.GetEnvironmentVariable("EmailPass"));
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
        }
    }
}
