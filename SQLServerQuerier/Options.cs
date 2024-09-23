using Common;
using DataLayer;
using DataLayer.Models;

namespace SQLServerQuerier
{
    public static class Options
    {
        public static async Task GetSimpleMovingAverageByTicker(string ticker)
        {
            List<TimeSeriesDailyEntity> data = await new TimeSeriesDailyRepository().GetTimeSeriesDailyByCount(ticker, 200);
            List<string> days10 = data.Take(10).ToList().Select(x => x.Close).ToList();
            double days10MovingAverage = MovingAverages.SimpleMovingAverage(days10);
            Console.WriteLine(String.Format("The 10 Day SMA for {0} is: {1}", ticker, days10MovingAverage));

            List<string> days50 = data.Take(50).ToList().Select(x => x.Close).ToList();
            double days50MovingAverage = MovingAverages.SimpleMovingAverage(days50);
            Console.WriteLine(String.Format("The 50 Day SMA for {0} is: {1}", ticker, days50MovingAverage));

            List<string> days200 = data.Take(200).ToList().Select(x => x.Close).ToList();
            double days200MovingAverage = MovingAverages.SimpleMovingAverage(days200);
            Console.WriteLine(String.Format("The 200 Day SMA for {0}, is : {1}", ticker, days200MovingAverage));
        }

        public static async Task GetLinearWeightedMovingAverageByTicker(string ticker)
        {
            List<TimeSeriesDailyEntity> data = await new TimeSeriesDailyRepository().GetTimeSeriesDailyByCount(ticker, 200);
            List<string> days10 = data.Take(10).ToList().Select(x => x.Close).ToList();
            double days10MovingAverage = MovingAverages.LinearWeightedMovingAverage(days10);
            Console.WriteLine(String.Format("The 10 Day LMWA for {0} is: {1}", ticker, days10MovingAverage));

            List<string> days50 = data.Take(50).ToList().Select(x => x.Close).ToList();
            double days50MovingAverage = MovingAverages.LinearWeightedMovingAverage(days50);
            Console.WriteLine(String.Format("The 50 Day LMWA for {0} is: {1}", ticker, days50MovingAverage));

            List<string> days200 = data.Take(200).ToList().Select(x => x.Close).ToList();
            double days200MovingAverage = MovingAverages.LinearWeightedMovingAverage(days200);
            Console.WriteLine(String.Format("The 200 Day LMWA for {0}, is : {1}", ticker, days200MovingAverage));
        }
    }
}
