using System.Globalization;
using TextTableBuilder.ObjectHandlers;
using TextTableBuilder.TableRenderers;
using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public class TableBuilder
{
    public TypeHandlerCollection TypeHandlers { get; } = new();
    public ObjectHandlerCollection ObjectHandlers { get; } = new();

    public static readonly ITableRenderer DefaultTableRenderer = new DefaultTableRenderer();

    public string Build(Table table)
        => Build(table, DefaultTableRenderer, CultureInfo.CurrentUICulture);

    public string Build(Table table, ITableRenderer tableRenderer)
        => Build(table, tableRenderer, CultureInfo.CurrentUICulture);

    public string Build(Table table, IFormatProvider formatProvider)
        => Build(table, DefaultTableRenderer, formatProvider);

    public string Build(Table table, ITableRenderer tableRenderer, IFormatProvider formatProvider)
    {
        if (table is null)
        {
            throw new ArgumentNullException(nameof(table));
        }

        if (tableRenderer is null)
        {
            throw new ArgumentNullException(nameof(tableRenderer));
        }

        if (formatProvider is null)
        {
            throw new ArgumentNullException(nameof(formatProvider));
        }

        var cols = table.Columns.ToArray();

        if (cols.Length == 0)
        {
            throw new InvalidOperationException("At least one column must be specified");
        }

        // Determine preliminary columnwidths
        var colwidths = cols.Select((c, i) => Math.Max(cols[i].MinWidth ?? 0, Math.Min(c.Width ?? int.MaxValue, c.Name.Length))).ToArray();  // Initialize column widths to header widths or minimum widths or fixed widths; whichever is larger

        // Iterate all rows, creating strings from all values and keep track of column widths
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
                    (v, i) => i >= cols.Length
                    ? throw new InvalidOperationException($"Number of values must match columns (row index: {rows.Count})")
                    : v is null
                        // Handle the special null-case with our NullHandler (if any, empty string otherwise)
                        ? (TypeHandlers.NullValueHandler?.Handle(formatProvider) ?? string.Empty)
                        // Use typehandler from colum when specified, else use typehandler from type from value
                        : (cols[i].TypeHandler ?? TypeHandlers.GetHandler(v.GetType())).Handle(v, formatProvider)).ToArray();

            // Make sure we have all cells
            if (rowvalues.Length != cols.Length)
            {
                throw new InvalidOperationException($"Number of values must match columns (row index: {rows.Count})");
            }

            // Add row to internal collection
            rows.Add(rowvalues);
            // Update column widths
            for (var i = 0; i < cols.Length; i++)
            {
                colwidths[i] = colwidths[i] < rowvalues[i].Length ? rowvalues[i].Length : colwidths[i];
            }
        }
        // For columns width a fixed width we may need to clamp the values
        colwidths = cols.Select((w, i) => Math.Min(w.Width ?? int.MaxValue, colwidths[i])).ToArray();

        // Now build actual table
        return tableRenderer.Render(
            Array.AsReadOnly(table.Columns.Select((c, i) => new RenderColumn(c.Name, colwidths[i], c.HeaderAlign, c.ValueAlign)).ToArray()),
            rows
        );
    }
}