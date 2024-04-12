using Common;
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
            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }
}
