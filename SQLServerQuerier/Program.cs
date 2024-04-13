// See https://aka.ms/new-console-template for more information
using SQLServerQuerier;

Console.Write("Please enter the ticker you wish run a calculation on historical data: ");
string userTickerInput = Console.ReadLine();

Console.WriteLine("Please select an option: ");
Console.WriteLine("1. Get SMA From Database by Ticker");
Console.WriteLine("2. Get LWMA From Database by Ticker");
string option = Console.ReadLine();

switch(option)
{
    case "1":
        await Options.GetSimpleMovingAverageByTicker(userTickerInput);
        break;
    case "2":
        await Options.GetLinearWeightedMovingAverageByTicker(userTickerInput);
        break;
    default:
        Console.WriteLine("You did not select a valid option");
        break;
}