namespace TextTableBuilder.TableRenderers;

public class DefaultTableRenderer : SimpleTableRenderer
{
    public DefaultTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base('|', '-', cellPadding, paddingChar) { }
}