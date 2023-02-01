using System.Data;
using Microsoft.ML;

namespace Practice.Models.Extensions;

public static class DataViewHelper
{
    public static DataTable? ToDataTable(this IDataView? dataView)
    {
        if (dataView == null) return null;
        var dt = new DataTable();
        var preview = dataView.Preview();
        foreach (var column in preview.Schema)
        {
            try
            {
                dt.Columns.Add(new DataColumn(column.Name));
            }
            catch (DuplicateNameException e)
            {
            }
        }
        
        foreach (var row in preview.RowView)
        {
            var r = dt.NewRow();
            foreach (var col in row.Values)
            {
                r[col.Key] = col.Value;
            }
            dt.Rows.Add(r);
        }
        return dt;
    }
}