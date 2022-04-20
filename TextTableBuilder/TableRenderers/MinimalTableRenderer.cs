namespace TextTableBuilder.TableRenderers;

public class MinimalTableRenderer : SimpleTableRenderer
{
    public MinimalTableRenderer(char columnSeparator = ' ', int cellPadding = 0, char paddingChar = DEFAULTPADDINGCHAR)
        : base(columnSeparator, null, cellPadding, paddingChar) { }
}
