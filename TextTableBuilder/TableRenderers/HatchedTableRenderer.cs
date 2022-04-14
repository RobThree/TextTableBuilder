namespace TextTableBuilder.TableRenderers;

public class HatchedTableRenderer : BorderedTableRenderer
{
    public HatchedTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base(@"/-+\|||+-++\-+/", cellPadding, paddingChar) { }
}