using CsvHelper.Configuration;

namespace DataAccess.Mappers;

public class SeasonalityFactorMap : ClassMap<SeasonalityFactor>
{
    public SeasonalityFactorMap()
    {
        Map(seasonalityFactor => seasonalityFactor.Id).Index(0);
        Map(seasonalityFactor => seasonalityFactor.Month).Index(1);
        Map(seasonalityFactor => seasonalityFactor.Factor).Index(2);
    }
}