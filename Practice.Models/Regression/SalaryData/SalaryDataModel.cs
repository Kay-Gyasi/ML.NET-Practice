using System.Data;
using System.Numerics;
using System.Security.AccessControl;
using Microsoft.ML;
using Microsoft.ML.Data;
using Practice.Models.Extensions;

namespace Practice.Models.Regression.SalaryData;

public class SalaryDataModel
{
    private readonly IDataView _data;
    private readonly IEnumerable<float> _predictions;
    public SalaryDataModel(params float[] yearsOfExperiences)
    {
        var context = new MLContext();

        _data = context.Data.LoadFromTextFile<SalaryData>(DataPath.SalaryData,
            hasHeader: true, separatorChar: ',');
        
        var pipeline = 
            context.Transforms.Concatenate("Features", 
                    nameof(SalaryDataInput.YearsExperience))
            .Append(context.Regression
            .Trainers
            .Sdca(nameof(SalaryData.Salary)));

        var transformedData = pipeline
            .Fit(_data);

        var predictionEngine = context.Model
            .CreatePredictionEngine<SalaryDataInput, SalaryDataOutput>(transformedData);
        
        // Evaluating the model
        // var pred = transformedData.Transform(_data);
        // var metrics = context.Regression.Evaluate(pred, nameof(SalaryData.Salary)
        //     , nameof(SalaryData.Salary));
        // Console.WriteLine("Mean Absolute Error: " + metrics.MeanAbsoluteError);
        // Console.WriteLine("Mean Squared Error: " + metrics.MeanSquaredError);
        // Console.WriteLine("Root Mean Squared Error: " + metrics.RootMeanSquaredError);
        // Console.WriteLine("RSquared: " + metrics.RSquared);

        _predictions = Enumerable.Empty<float>();

        foreach (var yearsOfExperience in yearsOfExperiences)
        {
            _predictions = _predictions.Append(predictionEngine
                .Predict(new SalaryDataInput(yearsOfExperience)).Salary);
        }
    }

    public DataTable? GetData() => _data.ToDataTable();
    public IEnumerable<float> GetPredictions() => _predictions;
}

public class SalaryData
{
    [LoadColumn(0)]
    [ColumnName("YearsExperience")]
    public float YearsOfExperience { get; set; }

    [LoadColumn(1)]
    public float Salary { get; set; }
}

public record SalaryDataInput(float YearsExperience);

public class SalaryDataOutput
{
    [ColumnName("Score")]
    public float Salary { get; set; }
}