using System.Globalization;
using ApplicationCore.Interfaces;
using ApplicationCore.Service;
using ConsoleApp.Controller;
using DataAccess.Repositories;

namespace ConsoleApp.Application;

internal class ConsoleApplication
{
    private static readonly string[] Commands = ["ads", "prediction", "demand"];

    private static readonly string HelpText = $"Commands:{Environment.NewLine}" +
                                              $"ads <id>{Environment.NewLine}" +
                                              $"prediction <id> <days>{Environment.NewLine}" +
                                              $"demand <id> <days>{Environment.NewLine}";

    private static readonly string ExitText = $"{Environment.NewLine}" +
                                              "X to exit, any other key to continue." +
                                              $"{Environment.NewLine}";

    private readonly OrderCalculationController _orderCalculationController;

    public ConsoleApplication(string salesDateFilePath, string seasonalityFactorFilePath)
    {
        // DI
        IUnitOfWork unitOfWork = new UnitOfWork(salesDateFilePath, seasonalityFactorFilePath);
        IOrderCalculationService orderCalculationService = new OrderCalculationService(unitOfWork);

        _orderCalculationController = new OrderCalculationController(orderCalculationService);
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine(HelpText);

            if (TryParseCommand(out var command, out var id, out var days))
            {
                var result = ExecuteCommand(command, id, days);
                Console.WriteLine(result);
            }

            Console.WriteLine(ExitText);

            var cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.X) break;
        }
    }

    private bool TryParseCommand(out string command, out long id, out int days)
    {
        command = "";
        id = -1;
        days = -1;

        var input = Console.ReadLine()?.Split();

        if (input is null || input.Length == 0)
        {
            Console.WriteLine("No command entered. Try again");
            return false;
        }

        command = Commands.Contains(input[0]) ? input[0] : string.Empty;
        if (string.IsNullOrEmpty(command))
        {
            Console.WriteLine("Unknown command. Try again.");
            return false;
        }

        if (!long.TryParse(input[1], out id))
        {
            Console.WriteLine("Invalid id format. Try again");
            return false;
        }

        if (!command.Equals("ads") && (input.Length < 3 || !int.TryParse(input[2], out days)))
        {
            Console.WriteLine("Invalid days format. Try again");
            return false;
        }

        return true;
    }

    private string? ExecuteCommand(string command, long id, int days)
    {
        switch (command)
        {
            case "ads":
                var ads = _orderCalculationController.GetAverageDailySales(id);
                return ads.Success ? ads.Data.ToString(CultureInfo.CurrentCulture) : ads.ErrorMessage;

            case "prediction":
                var prediction = _orderCalculationController.GetSalesPrediction(id, days);
                return prediction.Success
                    ? prediction.Data.ToString(CultureInfo.CurrentCulture)
                    : prediction.ErrorMessage;

            case "demand":
                var demand = _orderCalculationController.GetDemand(id, days);
                return demand.Success ? demand.Data.ToString(CultureInfo.CurrentCulture) : demand.ErrorMessage;
        }

        return "Unknown command";
    }
}