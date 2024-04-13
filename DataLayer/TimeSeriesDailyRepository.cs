using Common;
using Common.Models;
using DataLayer.Models;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static async Task<List<TimeSeriesDailyEntity>> GetTimeSeriesDailyByCount(string ticker, int count)
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionStrings.StocksDatabase);
            using SqlCommand command = new SqlCommand("dbo.GetTimeSeriesDailyByCount", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ticker", ticker);
            command.Parameters.AddWithValue("@Count", count);
            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<TimeSeriesDailyEntity> results = new();
            while (reader.Read())
            {
                results.Add(new TimeSeriesDailyEntity()
                {
                    ID = reader.GetGuid(reader.GetOrdinal("ID")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    Close = reader.GetString(reader.GetOrdinal("Close")),
                    High = reader.GetString(reader.GetOrdinal("High")),
                    Low = reader.GetString(reader.GetOrdinal("Low")),
                    Open = reader.GetString(reader.GetOrdinal("Open")),
                    Volumne = reader.GetString(reader.GetOrdinal("Volumne")),
                    Ticker = reader.GetGuid(reader.GetOrdinal("Ticker"))
                });
            }
            return results;
        }
    }
}
