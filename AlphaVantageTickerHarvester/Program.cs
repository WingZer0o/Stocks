// See https://aka.ms/new-console-template for more information
using AlphaVantageTickerHarvester;
using DataLayer;

Console.Write("Enter Ticker To Populate Local SQL Server: ");
string userTickerInput = Console.ReadLine();
TickerRepository.InsertTicker(userTickerInput);


Console.WriteLine("Please enter an option: ");
Console.WriteLine("1. 10, 50, 200 Day Moving Averages");

string userOption = Console.ReadLine();

switch(userOption)
{
    case "1":
        await Options.MovingAverage1050200Day(userTickerInput);
        break;
}

// populate the time series data for the ticker.
Console.ReadLine();