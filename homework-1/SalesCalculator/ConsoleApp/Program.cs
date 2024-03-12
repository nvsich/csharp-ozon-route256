using ConsoleApp.Application;

namespace ConsoleApp;

internal static class Program
{
    public static void Main()
    {
        var consoleApplication = new ConsoleApplication(
            salesDateFilePath: "salesDates.txt",
            seasonalityFactorFilePath: "seasonalityFactors.txt"
        );

        consoleApplication.Run();
    }
}