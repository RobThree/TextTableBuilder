using System.Collections.ObjectModel;
using System.Text;

namespace TextTableBuilder.TableRenderers;

public class BorderedTableRenderer : BaseTableRenderer
{
    private readonly string _chars;
    private readonly char _paddingchar;
    private readonly int _cellpadding;

    public BorderedTableRenderer(string borderChars, int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
    {
        if (borderChars.Length != 15)
        {
            throw new Exception($"{nameof(borderChars)} must be exactly 15 characters long");
        }

        _chars = borderChars;
        _cellpadding = cellPadding;
        _paddingchar = paddingChar;
    }

    public override string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows)
    {
        var sb = new StringBuilder();

        // Top border
        sb.AppendLine(RenderLine(_chars[0], _chars[2], _chars[3], _chars[1], columns, _cellpadding));

        // Headers
        sb.AppendLine($"{_chars[4]}{RenderRow(_chars[5], columns.Select(c => RenderCell(c.Name, c.HeaderAlign, c.Width, _paddingchar, _cellpadding)))}{_chars[6]}");

        // Header separator
        sb.AppendLine(RenderLine(_chars[7], _chars[9], _chars[10], _chars[8], columns, _cellpadding));

        // Rows
        foreach (var row in rows)
        {
            sb.AppendLine($"{_chars[4]}{RenderRow(_chars[5], row.Select((value, i) => RenderCell(value, columns[i].ValueAlign, columns[i].Width, _paddingchar, _cellpadding)))}{_chars[6]}");
        }

        // Bottom border
        sb.AppendLine(RenderLine(_chars[11], _chars[13], _chars[14], _chars[12], columns, _cellpadding));
        return sb.ToString();
    }
}