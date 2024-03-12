using ApplicationCore.Interfaces;
using DataAccess.Database;

namespace DataAccess.Repositories;

internal class SalesDateRepository : IRepository<SalesDate>
{
    private readonly DatabaseContext _dbContext;

    public SalesDateRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<SalesDate> Find(Func<SalesDate, bool> predicate)
    {
        return _dbContext.SalesDates.Where(predicate).ToList();
    }
}