using CsvHelper.Configuration;

namespace DataAccess.Mappers;

public class SalesDateMap : ClassMap<SalesDate>
{
    public SalesDateMap()
    {
        Map(salesDate => salesDate.Id).Index(0);
        Map(salesDate => salesDate.Date).Index(1);
        Map(salesDate => salesDate.SalesAmount).Index(2);
        Map(salesDate => salesDate.StockAmount).Index(3);
    }
}