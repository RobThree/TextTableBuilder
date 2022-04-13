namespace TextTableBuilder.TableRenderers;

public class MinimalTableRenderer : SimpleTableRenderer
{
    public MinimalTableRenderer(char columnSeparator = ' ', int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base(columnSeparator, null, cellPadding, paddingChar) { }
}
