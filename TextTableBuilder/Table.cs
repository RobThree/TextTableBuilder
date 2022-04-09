using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public class Table : IColumnAdd, IRowAdd
{
    private readonly List<Column> _columns = new();
    private readonly List<Row> _rows = new();

    public IReadOnlyCollection<Column> Columns => _columns;
    public IReadOnlyCollection<Row> Rows => _rows;

    public IColumnAdd AddColumn(string name, Align align = Align.Left, Align rowAlign = Align.Left, int? minWidth = null, ITypeHandler? typeHandler = null)
        => AddColumn(new Column(name, align, rowAlign, minWidth, typeHandler));

    public IColumnAdd AddColumn(Column column)
    {
        if (_rows.Count > 0)
        {
            throw new InvalidOperationException("Columns cannot be added after rows have been added");
        }

        _columns.Add(column);
        return this;
    }

    public IRowAdd AddRow(params object[] values)
        => AddRow(new ValueRow(values));

    public IRowAdd AddRow(ValueRow row)
    {
        if (row is null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        if (_columns.Count == 0)
        {
            throw new InvalidOperationException("Define columns first");
        }

        _rows.Add(row);

        return this;
    }

    public IRowAdd AddRow(ObjectRow row)
    {
        if (row is null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        if (_columns.Count == 0)
        {
            throw new InvalidOperationException("Define columns first");
        }

        if (row.Value is not null)
        {
            _rows.Add(row);
        }

        return this;
    }

    public IRowAdd AddRow<T>(T obj)
    {
        if (obj is not null)
        {
            return AddRow(new ObjectRow(obj));
        }

        return this;
    }
}
