using ApplicationCore.Exception;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;

[ApiController]
[Route("/api")]
public class OrderCalculationController : ControllerBase
{
    private readonly IOrderCalculationService _orderCalculationService;

    public OrderCalculationController(IOrderCalculationService orderCalculationService)
    {
        _orderCalculationService = orderCalculationService;
    }

    [HttpGet("ads/{id}")]
    public IActionResult GetAverageDailySales(long id)
    {
        try
        {
            var result = _orderCalculationService.CalculateAverageDailySales(id);
            return Ok(result);
        }
        catch (IdNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("prediction/{id:long}/{days:int}")]
    public IActionResult GetSalesPrediction(long id, int days)
    {
        try
        {
            var result = _orderCalculationService.CalculateSalesPrediction(id, days);
            return Ok(result);
        }
        catch (IdNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (InvalidInputException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("demand/{id:long}/{days:int}")]
    public IActionResult GetDemand(long id, int days)
    {
        try
        {
            var result = _orderCalculationService.CalculateDemand(id, days);
            return Ok(result);
        }
        catch (IdNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (InvalidInputException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}