using System.Data;
using Practice.Models.Data_Preprocessing;
using Practice.Models.Data_Preprocessing.AutoPass;
using Practice.Models.Extensions;
using Practice.Models.Regression.SalaryData;

/*
var autoPassModel = new AutoPassModel();
var table = autoPassModel.GetData().ToDataTable();

if (table?.Rows != null)
{
    foreach (DataRow row in table.Rows)
    {
        foreach (DataColumn column in table.Columns)
        {
            Console.Write(row[column] + "\t");
        }

        Console.WriteLine();
    }
}
*/

var yrs = new float[] { 6.4f, 2.6f, 8, 5.9f };
var salaryDataModel = new SalaryDataModel(yrs);
var predictions = salaryDataModel.GetPredictions().ToList();

for (var i = 0; i < yrs.Length; i++)
{
    Console.WriteLine($"{yrs[i]} - {predictions[i]}");
}

// var table = salaryDataModel.GetData();
//
// if (table?.Rows != null)
// {
//     foreach (DataRow row in table.Rows)
//     {
//         foreach (DataColumn column in table.Columns)
//         {
//             Console.Write(row[column] + "\t");
//         }
//
//         Console.WriteLine();
//     }
// }




