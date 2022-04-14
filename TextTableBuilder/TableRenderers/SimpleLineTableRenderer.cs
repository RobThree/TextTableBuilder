namespace TextTableBuilder.TableRenderers;

public class SimpleLineTableRenderer : BorderedTableRenderer
{
    public SimpleLineTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base("+-++|||+-+++-++", cellPadding, paddingChar) { }
}
