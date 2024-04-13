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

        public static double LinearWeightedMovingAverage(List<string> prices)
        {
            double total = 0;
            double weightSum = 0;
            double weight = prices.Count;
            foreach (string pr in prices)
            {
                total += (double.Parse(pr) * weight);
                weightSum += weight;
                weight--;
            }
            return total / weightSum;
        }
    }
}
