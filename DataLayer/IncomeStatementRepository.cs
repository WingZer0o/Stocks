using Common;
using Common.Models;
using System.Data.SqlClient;

namespace DataLayer
{
    public class IncomeStatementRepository
    {
        public async Task InsertIncomeStatement(string ticker, AnnualReport parsedAnnualReport)
        {
            using SqlConnection connection = new SqlConnection(Constants.ConnectionStrings.StocksDatabase);
            using SqlCommand command = new SqlCommand("dbo.InsertIncomeStatement", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@FiscalDateEnding", parsedAnnualReport.FiscalDateEnding);
            command.Parameters.AddWithValue("@ReportedCurrency", parsedAnnualReport.ReportedCurrency);
            command.Parameters.AddWithValue("@GrossProfit", parsedAnnualReport.GrossProfit);
            command.Parameters.AddWithValue("@TotalRevenue", parsedAnnualReport.TotalRevenue);
            command.Parameters.AddWithValue("@CostOfRevenue", parsedAnnualReport.CostOfRevenue);
            command.Parameters.AddWithValue("@CostofGoodsAndServicesSold", parsedAnnualReport.CostofGoodsAndServicesSold);
            command.Parameters.AddWithValue("@OperatingIncome", parsedAnnualReport.OperatingIncome);
            command.Parameters.AddWithValue("@SellingGeneralAndAdministrative", parsedAnnualReport.SellingGeneralAndAdministrative);
            command.Parameters.AddWithValue("@ResearchAndDevelopment", parsedAnnualReport.ResearchAndDevelopment);
            command.Parameters.AddWithValue("@OperatingExpenses", parsedAnnualReport.OperatingExpenses);
            command.Parameters.AddWithValue("@InvestmentIncomeNet", parsedAnnualReport.InvestmentIncomeNet);
            command.Parameters.AddWithValue("@NetInterestIncome", parsedAnnualReport.NetInterestIncome);
            command.Parameters.AddWithValue("@InterestIncome", parsedAnnualReport.InterestIncome);
            command.Parameters.AddWithValue("@InterestExpense", parsedAnnualReport.InterestExpense);
            command.Parameters.AddWithValue("@NonInterestIncome", parsedAnnualReport.NonInterestIncome);
            command.Parameters.AddWithValue("@OtherNonOperatingIncome", parsedAnnualReport.OtherNonOperatingIncome);
            command.Parameters.AddWithValue("@Depreciation", parsedAnnualReport.Depreciation);
            command.Parameters.AddWithValue("@DepreciationAndAmortization", parsedAnnualReport.DepreciationAndAmortization);
            command.Parameters.AddWithValue("@IncomeBeforeTax", parsedAnnualReport.IncomeBeforeTax);
            command.Parameters.AddWithValue("@IncomeTaxExpense", parsedAnnualReport.IncomeTaxExpense);
            command.Parameters.AddWithValue("@InterestAndDebtExpense", parsedAnnualReport.InterestAndDebtExpense);
            command.Parameters.AddWithValue("@NetIncomeFromContinuingOperations", parsedAnnualReport.NetIncomeFromContinuingOperations);
            command.Parameters.AddWithValue("@ComprehensiveIncomeNetOfTax", parsedAnnualReport.ComprehensiveIncomeNetOfTax);
            command.Parameters.AddWithValue("@Ebit", parsedAnnualReport.Ebit);
            command.Parameters.AddWithValue("@Ebitda", parsedAnnualReport.Ebitda);
            command.Parameters.AddWithValue("@NetIncome", parsedAnnualReport.NetIncome);
            command.Parameters.AddWithValue("@Ticker", ticker);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }
}
