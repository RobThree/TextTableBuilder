using System.Collections.ObjectModel;

namespace TextTableBuilder.TableRenderers;

public abstract class BaseTableRenderer : ITableRenderer
{
    public const char DEFAULTPADDINGCHAR = ' ';
    public const int DEFAULTCELLPADDING = 1;
    
    public abstract string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows);

    protected static string RenderRow(char columnSeparator, IEnumerable<string> values, char paddingChar, int cellPadding)
        => string.Join(columnSeparator.ToString(), values);

    protected static string RenderLine(char left, char mid, char right, char pad, ReadOnlyCollection<RenderColumn> widths, int cellPadding)
        => $"{left}{string.Join(mid.ToString(), widths.Select(c => new string(pad, c.Width + (cellPadding * 2))))}{right}";

    protected static string RenderCell(string value, Align align, int width, char paddingChar, int cellPadding)
        => PadCell(AlignString(TruncateString(value, align, width), align, width, paddingChar), paddingChar, cellPadding);

    protected static string PadCell(string value, char paddingChar, int count)
        => value.PadLeft(value.Length + count, paddingChar).PadRight(value.Length + (count * 2), paddingChar);

    protected static string AlignString(string value, Align align, int width, char paddingChar)
        => align switch
        {
            Align.Right => value.PadLeft(width, paddingChar),
            Align.Center => value.PadLeft((width - value.Length) / 2 + value.Length, paddingChar).PadRight(width, paddingChar),
            _ => value.PadRight(width, paddingChar)
        };

    protected static string TruncateString(string value, Align align, int length)
        => align switch
        {
            Align.Right => value.Substring(Math.Max(value.Length - length, 0), Math.Min(value.Length, length)),
            Align.Center => value.Substring(Math.Max((value.Length - length) / 2, 0), Math.Min(length, value.Length)),
            _ => value.Substring(0, Math.Min(length, value.Length))
        };
}
