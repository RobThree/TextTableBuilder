namespace TextTableBuilder.TableRenderers;

public class RoundedCornersTableRenderer : BorderedTableRenderer
{
    public RoundedCornersTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base("╭─┬╮│││├─┼┤╰─┴╯", cellPadding, paddingChar) { }
}