using Common;
using DataLayer.Models;
using System.Data.SqlClient;

namespace DataLayer
{
    public static class TickerRepository
    {
        public static async Task InsertTicker(string ticker)
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionStrings.StocksDatabase);
            using SqlCommand command = new SqlCommand("dbo.InsertTicker", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Ticker", ticker);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public static async Task<List<TickerEntity>> GetPortfolioTickers()
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionStrings.StocksDatabase);
            using SqlCommand command = new SqlCommand("dbo.GetPortfolioTickers", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            List<TickerEntity> tickers = new();
            while (await reader.ReadAsync())
            {
                tickers.Add(new TickerEntity()
                {
                    ID = reader.GetGuid(reader.GetOrdinal("ID")),
                    Ticker = reader.GetString(reader.GetOrdinal("Ticker")),
                    IsInPortfolio = reader.GetBoolean(reader.GetOrdinal("IsInPortfolio"))
                });
            }
            return tickers;
        }
    }
}
