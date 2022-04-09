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
        => Build(table, TableStyle.Default);

    public string Build(Table table, TableStyle tableStyle)
        => Build(table, tableStyle, CultureInfo.CurrentUICulture);
    public string Build(Table table, IFormatProvider formatProvider)
        => Build(table, TableStyle.Default, formatProvider);

    public string Build(Table table, TableStyle tableStyle, IFormatProvider formatProvider)
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
                        // Handle the special null-case with our NullHandler (if any, empty string otherwise)
                        ? (TypeHandlers.NullValueHandler?.Handle(formatProvider) ?? string.Empty)
                        // Use typehandler from colum when specified, else use typehandler from type from value
                        : (cols[i].TypeHandler ?? TypeHandlers.GetHandler(v.GetType())).Handle(v, formatProvider)).ToArray();

            // Make sure we have all cells
            if (rowvalues.Length != cols.Length)
            {
                throw new InvalidOperationException($"Number of values must match columns (row {rows.Count})");
            }
            // Add row to internal collection
            rows.Add(rowvalues);
            // Update column widths
            for (var i = 0; i < cols.Length; i++)
            {
                colwidths[i] = colwidths[i] < rowvalues[i].Length ? rowvalues[i].Length : colwidths[i];
            }
        }

        // Now build actual table
        var sb = new StringBuilder();

        // Headers
        sb.AppendLine(MakeRow(tableStyle, table.Columns.Select((col, i) => AlignString(col.Name, col.Align, colwidths[i], tableStyle.Padding))));

        // Header separator
        if (tableStyle.HeaderSeparator is not null)
        {
            sb.AppendLine(MakeRow(tableStyle, colwidths.Select(w => new string(tableStyle.HeaderSeparator.Value, w))));
        }

        // Rows
        foreach (var row in rows)
        {
            sb.AppendLine(MakeRow(tableStyle, row.Select((value, i) => AlignString(value, cols[i].RowAlign, colwidths[i], tableStyle.Padding))));
        }
        return sb.ToString();
    }

    private static string MakeRow(TableStyle tableStyle, IEnumerable<string> values)
        => string.Join(tableStyle.ColumnSeparator, values);

    private static string AlignString(string value, Align align, int width, char paddingChar)
        => align switch
        {
            Align.Right => value.PadLeft(width, paddingChar),
            Align.Center => value.PadLeft((width - value.Length) / 2 + value.Length, paddingChar).PadRight(width, paddingChar),
            _ => value.PadRight(width, paddingChar)
        };
}