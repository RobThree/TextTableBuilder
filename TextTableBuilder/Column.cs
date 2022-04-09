using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public record Column(
    string Name,
    Align Align = Align.Left,
    Align RowAlign = Align.Left,
    int? MinWidth = null,
    ITypeHandler? TypeHandler = null
);
