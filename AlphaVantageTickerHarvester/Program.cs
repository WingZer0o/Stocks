// See https://aka.ms/new-console-template for more information
using AlphaVantageTickerHarvester;
using DataLayer;

Console.Write("Enter Ticker To Populate Local SQL Server: ");
string userTickerInput = Console.ReadLine();
TickerRepository.InsertTicker(userTickerInput);


Console.WriteLine("Please enter an option: ");
Console.WriteLine("1. 10, 50, 200 Day Simple Moving Averages (SMA)");
Console.WriteLine("2. Populate Database with Time Series Data");
Console.WriteLine("3. Populate Portfolio Tickers with Time Series Data");
Console.WriteLine("4. Populate Database with Income Statement Data");

string userOption = Console.ReadLine();

switch (userOption)
{
    case "1":
        await Options.MovingAverage1050200Day(userTickerInput);
        break;
    case "2":
        await Options.PopulateDataWithTimeSeriesData(userTickerInput);
        break;
    case "3":
        await Options.PopulatePortfolioTickersWithTimeSeriesData();
        break;
    case "4":
        await Options.PopulateIncomeStatementDataForTicker(userTickerInput);
        break;
    default:
        Console.WriteLine("You did not enter a valid option");
        break;
}

// populate the time series data for the ticker.
Console.ReadLine();