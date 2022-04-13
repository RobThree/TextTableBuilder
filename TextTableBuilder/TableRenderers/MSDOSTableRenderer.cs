namespace TextTableBuilder.TableRenderers;

public class MSDOSTableRenderer : SimpleTableRenderer
{
    public MSDOSTableRenderer(int cellPadding = DEFAULTCELLPADDING, char paddingChar = DEFAULTPADDINGCHAR)
        : base('║', '═', cellPadding, paddingChar) { }
}