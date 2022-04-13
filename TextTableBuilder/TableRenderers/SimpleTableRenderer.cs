using System.Collections.ObjectModel;
using System.Text;

namespace TextTableBuilder.TableRenderers;

public abstract class SimpleTableRenderer : BaseTableRenderer
{
    private readonly char _paddingchar;
    private readonly char _columnseparator;
    private readonly char? _rowseparator;
    private readonly int _cellpadding;

    public SimpleTableRenderer(char columnSeparator, char? rowSeparator, int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
    {
        _paddingchar = paddingChar;
        _columnseparator = columnSeparator;
        _rowseparator = rowSeparator;
        _cellpadding = cellPadding;
    }

    public override string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows)
    {
        var sb = new StringBuilder();

        // Headers
        sb.AppendLine(RenderRow(_columnseparator, columns.Select(c => RenderCell(c.Name, c.HeaderAlign, c.Width, _paddingchar, _cellpadding)), _paddingchar, _cellpadding));

        // Header separator
        if (_rowseparator is not null)
        {
            sb.AppendLine(RenderRow(_columnseparator, columns.Select(c => new string(_rowseparator.Value, c.Width + (_cellpadding * 2))), _paddingchar, _cellpadding));
        }

        // Rows
        foreach (var row in rows)
        {
            sb.AppendLine(RenderRow(_columnseparator, row.Select((value, i) => RenderCell(value, columns[i].ValueAlign, columns[i].Width, _paddingchar, _cellpadding)), _paddingchar, _cellpadding));
        }
        return sb.ToString();
    }
}
