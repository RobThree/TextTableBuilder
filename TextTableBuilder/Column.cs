using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public record Column(
    string Name,
    Align Align = Column.DefaultAlign,
    Align RowAlign = Row.DefaultAlign,
    int? MinWidth = null,
    ITypeHandler? TypeHandler = null
)
{
    public const Align DefaultAlign = Align.Left;

    public static Column FromName(string name)
    {
        var calign = Column.DefaultAlign;
        var ralign = Row.DefaultAlign;

        if (name.Length > 1)
        {
            switch (name[0])
            {
                case char s when s == '^' || s == '~':
                    calign = GetAlignFromChar(name[0]);
                    name = name.Substring(1);
                    break;
            }
        }

        if (name.Length > 1)
        {
            switch (name[name.Length - 1])
            {
                case char s when s == '^' || s == '~':
                    ralign = GetAlignFromChar(name[name.Length - 1]);
                    name = name.Substring(0, name.Length - 1);
                    break;
            }
        }

        return new Column(name, calign, ralign);
    }

    private static Align GetAlignFromChar(char align)
        => align switch
        {
            '^' => Align.Right,
            '~' => Align.Center,
            _ => Align.Left
        };
}
