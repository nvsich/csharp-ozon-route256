using ApplicationCore.Exception;
using ApplicationCore.Interfaces;
using ConsoleApp.Model;

namespace ConsoleApp.Controller;

internal class OrderCalculationController
{
    private readonly IOrderCalculationService _orderCalculationService;

    public OrderCalculationController(IOrderCalculationService orderCalculationService)
    {
        _orderCalculationService = orderCalculationService;
    }

    public ActionResult<double> GetAverageDailySales(long id)
    {
        try
        {
            var result = _orderCalculationService.CalculateAverageDailySales(id);
            return new ActionResult<double>(result, true);
        }
        catch (IdNotFoundException e)
        {
            return new ActionResult<double>(e.Message, false);
        }
    }

    public ActionResult<double> GetSalesPrediction(long id, int days)
    {
        try
        {
            var result = _orderCalculationService.CalculateSalesPrediction(id, days);
            return new ActionResult<double>(result, true);
        }
        catch (IdNotFoundException e)
        {
            return new ActionResult<double>(e.Message, false);
        }
        catch (InvalidInputException e)
        {
            return new ActionResult<double>(e.Message, false);
        }
    }

    public ActionResult<double> GetDemand(long id, int days)
    {
        try
        {
            var result = _orderCalculationService.CalculateDemand(id, days);
            return new ActionResult<double>(result, true);
        }
        catch (IdNotFoundException e)
        {
            return new ActionResult<double>(e.Message, false);
        }
        catch (InvalidInputException e)
        {
            return new ActionResult<double>(e.Message, false);
        }
    }
}