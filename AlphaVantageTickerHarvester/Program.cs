// See https://aka.ms/new-console-template for more information
using AlphaVantageTickerHarvester;
using DataLayer;

Console.WriteLine("Please enter an option: ");
Console.WriteLine("1. 10, 50, 200 Day Simple Moving Averages (SMA)");
Console.WriteLine("2. Populate Database with Time Series Data");
Console.WriteLine("3. Populate Portfolio Tickers with Time Series Data");
Console.WriteLine("4. Populate Database with Income Statement Data");
string userOption = Console.ReadLine();

switch (userOption)
{
    case "1":
        Console.Write("Enter Ticker To Populate Local SQL Server: ");
        string userTickerInput = Console.ReadLine();
        new TickerRepository().InsertTicker(userTickerInput);
        await Options.MovingAverage1050200Day(userTickerInput);
        break;
    case "2":
        Console.Write("Enter Ticker To Populate Local SQL Server: ");
        string userTickerInput2 = Console.ReadLine();
        new TickerRepository().InsertTicker(userTickerInput2);
        await Options.PopulateDataWithTimeSeriesData(userTickerInput2);
        break;
    case "3":
        await Options.PopulatePortfolioTickersWithTimeSeriesData();
        break;
    case "4":
        Console.Write("Enter Ticker To Populate Local SQL Server: ");
        string userTickerInput3 = Console.ReadLine();
        new TickerRepository().InsertTicker(userTickerInput3);
        await Options.PopulateIncomeStatementDataForTicker(userTickerInput3);
        break;
    default:
        Console.WriteLine("You did not enter a valid option");
        break;
}

// populate the time series data for the ticker.
Console.ReadLine();