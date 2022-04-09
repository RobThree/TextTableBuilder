namespace TextTableBuilder;

public record TableStyle(char Padding, string ColumnSeparator, char? HeaderSeparator)
{
    public static readonly TableStyle Default = new(' ', " | ", '-');
    public static readonly TableStyle Minimal = new(' ', " ", null);
    public static readonly TableStyle MSDOS = new(' ', "║", '═');
}