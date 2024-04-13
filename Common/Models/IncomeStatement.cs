using Newtonsoft.Json;

namespace Common.Models
{
    public class AnnualReport
    {
        [JsonProperty("fiscalDateEnding")]
        public DateTime FiscalDateEnding { get; set; }

        [JsonProperty("reportedCurrency")]
        public string ReportedCurrency { get; set; }

        [JsonProperty("grossProfit")]
        public string GrossProfit { get; set; }

        [JsonProperty("totalRevenue")]
        public string TotalRevenue { get; set; }

        [JsonProperty("costOfRevenue")]
        public string CostOfRevenue { get; set; }

        [JsonProperty("costofGoodsAndServicesSold")]
        public string CostofGoodsAndServicesSold { get; set; }

        [JsonProperty("operatingIncome")]
        public string OperatingIncome { get; set; }

        [JsonProperty("sellingGeneralAndAdministrative")]
        public string SellingGeneralAndAdministrative { get; set; }

        [JsonProperty("researchAndDevelopment")]
        public string ResearchAndDevelopment { get; set; }

        [JsonProperty("operatingExpenses")]
        public string OperatingExpenses { get; set; }

        [JsonProperty("investmentIncomeNet")]
        public string InvestmentIncomeNet { get; set; }

        [JsonProperty("netInterestIncome")]
        public string NetInterestIncome { get; set; }

        [JsonProperty("interestIncome")]
        public string InterestIncome { get; set; }

        [JsonProperty("interestExpense")]
        public string InterestExpense { get; set; }

        [JsonProperty("nonInterestIncome")]
        public string NonInterestIncome { get; set; }

        [JsonProperty("otherNonOperatingIncome")]
        public string OtherNonOperatingIncome { get; set; }

        [JsonProperty("depreciation")]
        public string? Depreciation { get; set; }

        [JsonProperty("depreciationAndAmortization")]
        public string DepreciationAndAmortization { get; set; }

        [JsonProperty("incomeBeforeTax")]
        public string IncomeBeforeTax { get; set; }

        [JsonProperty("incomeTaxExpense")]
        public string IncomeTaxExpense { get; set; }

        [JsonProperty("interestAndDebtExpense")]
        public string InterestAndDebtExpense { get; set; }

        [JsonProperty("netIncomeFromContinuingOperations")]
        public string NetIncomeFromContinuingOperations { get; set; }

        [JsonProperty("comprehensiveIncomeNetOfTax")]
        public string ComprehensiveIncomeNetOfTax { get; set; }

        [JsonProperty("ebit")]
        public string Ebit { get; set; }

        [JsonProperty("ebitda")]
        public string Ebitda { get; set; }

        [JsonProperty("netIncome")]
        public string NetIncome { get; set; }
    }
    public class IncomeStatement
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("annualReports")]
        public List<AnnualReport> AnnualReports { get; set; }
    }
}
