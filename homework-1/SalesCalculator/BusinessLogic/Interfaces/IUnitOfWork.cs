namespace ApplicationCore.Interfaces;

public interface IUnitOfWork
{
    IRepository<SalesDate> SalesDates { get; }
    IRepository<SeasonalityFactor> SeasonalityFactors { get; }
}