using ApplicationCore.Exception;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Service;

public class OrderCalculationService : IOrderCalculationService
{
    private readonly IUnitOfWork _database;

    public OrderCalculationService(IUnitOfWork unitOfWork)
    {
        _database = unitOfWork;
    }

    public double CalculateAverageDailySales(long id)
    {
        var productSales = _database
            .SalesDates
            .Find(sale => sale.Id == id);

        if (productSales.Count == 0) throw new IdNotFoundException("No records about product with given ID");

        var totalSales = productSales
            .Where(s => s.StockAmount != 0)
            .Sum(s => s.SalesAmount);

        var daysInStockAmount = productSales
            .Where(s => s.StockAmount != 0)
            .ToList()
            .Count;

        return daysInStockAmount == 0
            ? 0
            : (double)totalSales / daysInStockAmount;
    }

    public double CalculateSalesPrediction(long id, int days)
    {
        if (days < 0) throw new InvalidInputException("Number of days can not be negative.");

        var ads = CalculateAverageDailySales(id);

        var lastSalesDate = _database
            .SalesDates
            .Find(x => x.Id == id)
            .MaxBy(x => x.Date);

        if (lastSalesDate == null) throw new IdNotFoundException("SaleDate with given id is not found");

        var lastDate = lastSalesDate.Date;
        var currentDate = lastDate;
        var seasonalityFactorSum = 0.0;

        for (var i = 0; i < days; i++)
        {
            var month = currentDate.Month;

            var currentSeasonalityFactor = _database
                .SeasonalityFactors
                .Find(sf => sf.Id == id && sf.Month == month)
                .FirstOrDefault()?.Factor ?? 1.0;

            seasonalityFactorSum += currentSeasonalityFactor;
            currentDate.AddDays(1);
        }

        return seasonalityFactorSum * ads;
    }

    public double CalculateDemand(long id, int days)
    {
        var prediction = CalculateSalesPrediction(id, days);

        var amountFromLastRecord = _database
            .SalesDates
            .Find(x => x.Id == id)
            .OrderBy(x => x.Date)
            .Last()
            .StockAmount;

        return prediction - amountFromLastRecord;
    }
}