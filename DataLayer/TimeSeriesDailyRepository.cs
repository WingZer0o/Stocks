using Common;
using Common.Models;
using System.Data.SqlClient;

namespace DataLayer
{
    public static class TimeSeriesDailyRepository
    {
        public static async Task InsertTimeSeriesDaily(string ticker, string date, TimeSeriesData parsedData)
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionStrings.StocksDatabase);
            using SqlCommand command = new SqlCommand("dbo.InsertTimeSeriesDaily", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ticker", ticker);
            command.Parameters.AddWithValue("@Date", date);
            command.Parameters.AddWithValue("@Open", parsedData.Open);
            command.Parameters.AddWithValue("@High", parsedData.High);
            command.Parameters.AddWithValue("@Low", parsedData.Low);
            command.Parameters.AddWithValue("@Close", parsedData.Close);
            command.Parameters.AddWithValue("@Volumne", parsedData.Volume);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
