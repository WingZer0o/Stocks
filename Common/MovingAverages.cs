namespace Common
{
    public static class MovingAverages
    {
        public static double SimpleMovingAverage(List<string> prices)
        {
            double total = 0;
            foreach (string pr in prices)
            {
                total += double.Parse(pr);
            }
            return total / prices.Count;
        }
    }
}
