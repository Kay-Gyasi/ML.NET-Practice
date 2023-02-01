using System.Data;
using Practice.Models.Data_Preprocessing;
using Practice.Models.Data_Preprocessing.AutoPass;
using Practice.Models.Extensions;

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



