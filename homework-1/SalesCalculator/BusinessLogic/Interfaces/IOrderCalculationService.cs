namespace ApplicationCore.Interfaces;

public interface IOrderCalculationService
{
    double CalculateAverageDailySales(long id);

    double CalculateSalesPrediction(long id, int days);

    double CalculateDemand(long id, int days);
}