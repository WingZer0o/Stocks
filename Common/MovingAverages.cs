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

        /// <summary>
        /// Assigns more weight to more recent prices, method assumes the prices where already sorted by Date in descending order.
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
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
