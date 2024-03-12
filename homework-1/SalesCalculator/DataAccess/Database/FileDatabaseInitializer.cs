using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DataAccess.Mappers;

namespace DataAccess.Database;

internal class FileDatabaseInitializer
{
    private readonly string _salesDateFilePath;
    private readonly string _seasonalityFactorFilePath;

    public FileDatabaseInitializer(string salesDateFilePath, string seasonalityFactorFilePath)
    {
        _salesDateFilePath = salesDateFilePath;
        _seasonalityFactorFilePath = seasonalityFactorFilePath;
    }

    public List<SalesDate> ReadSalesDate()
    {
        try
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using var reader = new StreamReader(_salesDateFilePath);
            using var csv = new CsvReader(reader, configuration);
            csv.Context.RegisterClassMap<SalesDateMap>();

            return csv.GetRecords<SalesDate>().ToList();
        }
        catch (Exception)
        {
            return [];
        }
    }

    public List<SeasonalityFactor> ReadSeasonalityFactors()
    {
        try
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            using var reader = new StreamReader(_seasonalityFactorFilePath);
            using var csv = new CsvReader(reader, configuration);
            csv.Context.RegisterClassMap<SeasonalityFactorMap>();

            return csv.GetRecords<SeasonalityFactor>().ToList();
        }
        catch (Exception)
        {
            return [];
        }
    }
}