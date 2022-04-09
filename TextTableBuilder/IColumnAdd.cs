using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public interface IColumnAdd : IRowAdd
{
    IColumnAdd AddColumn(string name, Align align = Align.Left, Align rowAlign = Align.Left, int? minWidth = null, ITypeHandler? typeHandler = null);
    IColumnAdd AddColumn(Column column);
    IColumnAdd AddColumns(IEnumerable<Column> columns);
}
