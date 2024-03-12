using ApplicationCore.Interfaces;
using DataAccess.Database;

namespace DataAccess.Repositories;

internal class SeasonalityFactorRepository : IRepository<SeasonalityFactor>
{
    private readonly DatabaseContext _dbContext;

    public SeasonalityFactorRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<SeasonalityFactor> Find(Func<SeasonalityFactor, bool> predicate)
    {
        return _dbContext.SeasonalityFactors.Where(predicate).ToList();
    }
}