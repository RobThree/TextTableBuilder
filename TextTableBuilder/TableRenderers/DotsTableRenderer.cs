namespace TextTableBuilder.TableRenderers;

public class DotsTableRenderer : BorderedTableRenderer {
    public DotsTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base("....::::.::....", cellPadding, paddingChar) { }
}