using Microsoft.ML;
using Microsoft.ML.Data;
using Practice.Models.Extensions;

namespace Practice.Models.Data_Preprocessing;

public class AutoPassModel
{
    private readonly IDataView _data;
    public AutoPassModel()
    {
        var context = new MLContext();
        var loader = context.Data.CreateTextLoader(new[]
        {
            new TextLoader.Column("Carname", DataKind.String, 0),
            new TextLoader.Column("Color", DataKind.String, 1),
            new TextLoader.Column("Age", DataKind.Single, 2),
            new TextLoader.Column("Speed", DataKind.Single, 3),
            new TextLoader.Column("AutoPass", DataKind.String, 4)
        }, hasHeader: true, separatorChar: ',');

        var data = loader.Load(DataPath.AutoPass);

        var pipeline = context.Transforms.Categorical.OneHotEncoding("Carname",
                "Carname")
            .Append(context.Transforms.Categorical.OneHotEncoding("Color", "Color"))
            .Append(context.Transforms.NormalizeMinMax("Age", "Age"))
            .Append(context.Transforms.NormalizeMinMax("Speed", "Speed"))
            .Append(context.Transforms.Conversion.MapValueToKey("AutoPass", "AutoPass"))
            .Append(context.Transforms.Concatenate("Features", new[] { "Carname", "Color", "Age", "Speed"}));

        var model = pipeline.Fit(data);
        _data = model.Transform(data);
    }

    public IDataView GetData() => _data;
}