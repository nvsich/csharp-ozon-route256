namespace DataAccess.Database;

internal class DatabaseContext
{
    public DatabaseContext(string salesDateFilePath, string seasonalityFactorFilePath)
    {
        var fileDatabaseInitializer = new FileDatabaseInitializer(salesDateFilePath, seasonalityFactorFilePath);
        SalesDates = fileDatabaseInitializer.ReadSalesDate();
        SeasonalityFactors = fileDatabaseInitializer.ReadSeasonalityFactors();
    }

    public List<SalesDate> SalesDates { get; }
    public List<SeasonalityFactor> SeasonalityFactors { get; }
}