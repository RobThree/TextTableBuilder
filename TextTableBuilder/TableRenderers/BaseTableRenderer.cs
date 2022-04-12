using System.Collections.ObjectModel;

namespace TextTableBuilder.TableRenderers;

public abstract class BaseTableRenderer : ITableRenderer
{
    public abstract string Render(ReadOnlyCollection<RenderColumn> columns, IEnumerable<string[]> rows);

    protected static string RenderRow(string columnSeparator, IEnumerable<string> values)
        => string.Join(columnSeparator, values);

    protected static string RenderCell(string value, Align align, int width, char paddingChar)
        => AlignString(TruncateString(value, align, width), align, width, paddingChar);

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
