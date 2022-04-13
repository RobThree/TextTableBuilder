namespace TextTableBuilder.TableRenderers;

public class SingleLineTableRenderer : BorderedTableRenderer {
    public SingleLineTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base("┌─┬┐│││├─┼┤└─┴┘", cellPadding, paddingChar) { }
}
