namespace TextTableBuilder.TableRenderers;

public class DoubleLineTableRenderer : BorderedTableRenderer
{
    public DoubleLineTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base("╔═╦╗║║║╠═╬╣╚═╩╝", cellPadding, paddingChar) { }
}
