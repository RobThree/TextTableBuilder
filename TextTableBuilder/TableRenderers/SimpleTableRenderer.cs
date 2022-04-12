using System.Collections.ObjectModel;
using System.Text;

namespace TextTableBuilder.TableRenderers;

public abstract class SimpleTableRenderer : BaseTableRenderer
{
    private readonly char _paddingchar;
    private readonly string _columnseparator;
    private readonly char? _rowseparator;

    public SimpleTableRenderer(char paddingChar, string columnSeparator, char? rowSeparator)
    {
        _paddingchar = paddingChar;
        _columnseparator = columnSeparator;
        _rowseparator = rowSeparator;
    }

    public override string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows)
    {
        var sb = new StringBuilder();

        // Headers
        sb.AppendLine(RenderRow(_columnseparator, columns.Select(c => RenderCell(c.Name, c.HeaderAlign, c.Width, _paddingchar))));

        // Header separator
        if (_rowseparator is not null)
        {
            sb.AppendLine(RenderRow(_columnseparator, columns.Select(c => new string(_rowseparator.Value, c.Width))));
        }

        // Rows
        foreach (var row in rows)
        {
            sb.AppendLine(RenderRow(_columnseparator, row.Select((value, i) => RenderCell(value, columns[i].ValueAlign, columns[i].Width, _paddingchar))));
        }
        return sb.ToString();
    }
}
