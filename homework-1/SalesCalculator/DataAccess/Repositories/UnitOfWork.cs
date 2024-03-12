using ApplicationCore.Interfaces;
using DataAccess.Database;

namespace DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;
    private SalesDateRepository? _salesDateRepository;
    private SeasonalityFactorRepository? _seasonalityFactorRepository;

    public UnitOfWork(string salesDateFilePath, string seasonalityFactorFilePath)
    {
        _dbContext = new DatabaseContext(salesDateFilePath, seasonalityFactorFilePath);
    }

    public IRepository<SalesDate> SalesDates
    {
        get { return _salesDateRepository ??= new SalesDateRepository(_dbContext); }
    }

    public IRepository<SeasonalityFactor> SeasonalityFactors
    {
        get { return _seasonalityFactorRepository ??= new SeasonalityFactorRepository(_dbContext); }
    }
}