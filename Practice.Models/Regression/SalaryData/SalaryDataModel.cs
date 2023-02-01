using System.Data;
using Microsoft.ML;
using Microsoft.ML.Data;
using Practice.Models.Extensions;

namespace Practice.Models.Regression.SalaryData;

public class SalaryDataModel
{
    private readonly IDataView _data;
    public SalaryDataModel()
    {
        var context = new MLContext();

        var loader = context.Data.CreateTextLoader(new[]
        {
            new TextLoader.Column("YearsExperience", DataKind.Single, 0),
            new TextLoader.Column("Salary", DataKind.Double, 1)
        }, hasHeader: true, separatorChar: ',');

        _data = loader.Load(DataPath.SalaryData);
    }

    public DataTable? GetData() => _data.ToDataTable();
}