using System.Globalization;
using System.Text;
using TextTableBuilder.ObjectHandlers;
using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public class TableBuilder
{
    public TypeHandlerCollection TypeHandlers { get; } = new();
    public ObjectHandlerCollection ObjectHandlers { get; } = new();

    public string Build(Table table)
        => Build(table, CultureInfo.CurrentUICulture);

    public string Build(Table table, IFormatProvider formatProvider)
    {
        if (table is null)
        {
            throw new ArgumentNullException(nameof(table));
        }

        if (formatProvider is null)
        {
            throw new ArgumentNullException(nameof(formatProvider));
        }

        // Iterate all rows, creating strings from all values and keep track of column widths
        var cols = table.Columns.ToArray();
        var colwidths = cols.Select((c, i) => Math.Max(cols[i].MinWidth ?? 0, c.Name.Length)).ToArray();  // Initialize column widths to header widths or minimum widths; whichever is larger
        var rows = new List<string[]>(table.Rows.Count);
        foreach (var row in table.Rows)
        {
            // Make string array from row values
            var rowvalues = (row switch
            {
                ValueRow vr => vr.Values,
                ObjectRow or => (ObjectHandlers.GetHandler(or.Value.GetType()) ?? ObjectHandlers.GetHandler(typeof(object))).Handle(or.Value, cols.Length),
                _ => throw new InvalidOperationException("Unknown rowtype")
            }).Select(
                    (v, i) => v is null
                        // Null is just an empty string
                        ? string.Empty
                        // Use typehandler from colum when specified, else use typehandler from type from value
                        : (cols[i].TypeHandler ?? TypeHandlers.GetHandler(v.GetType())).Handle(v, formatProvider)).ToArray();

            if (rowvalues.Length != cols.Length)
            {
                throw new InvalidOperationException($"Number of values must match columns (row {rows.Count})");
            }

            rows.Add(rowvalues);

            for (var i = 0; i < cols.Length; i++)
            {
                colwidths[i] = colwidths[i] < rowvalues[i].Length ? rowvalues[i].Length : colwidths[i];
            }
        }

        // Now build actual table
        var sb = new StringBuilder();
        sb.AppendLine(MakeRow(table.Columns.Select((col, i) => AlignString(col.Name, col.Align, colwidths[i]))));
        sb.AppendLine(MakeRow(colwidths.Select(w => new string('-', w))));
        foreach (var row in rows)
        {
            sb.AppendLine(MakeRow(row.Select((value, i) => AlignString(value, cols[i].RowAlign, colwidths[i]))));
        }
        return sb.ToString();
    }

    private static string MakeRow(IEnumerable<string> values)
        => string.Join(" | ", values);

    private static string AlignString(string value, Align align, int width, char paddingChar = ' ')
        => align switch
        {
            Align.Right => value.PadLeft(width, paddingChar),
            Align.Center => value.PadLeft((width - value.Length) / 2 + value.Length, paddingChar).PadRight(width, paddingChar),
            _ => value.PadRight(width, paddingChar)
        };
}
