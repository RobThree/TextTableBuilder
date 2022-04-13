using TextTableBuilder.TypeHandlers;

namespace TextTableBuilder;

public class Table
{
    private readonly List<Column> _columns = new();
    private readonly List<Row> _rows = new();

    public IReadOnlyCollection<Column> Columns => _columns;
    public IReadOnlyCollection<Row> Rows => _rows;

    public Table() { }
    public Table(IEnumerable<Column> columns)
        : this(columns, Enumerable.Empty<Row>()) { }
        
    public Table(IEnumerable<Column> columns, IEnumerable<Row> rows) 
        => AddColumns(columns).AddRow(rows);   

    public Table AddColumn(string name, Align align = Align.Left, Align rowAlign = Align.Left, int? minWidth = null, int? width = null, ITypeHandler? typeHandler = null)
        => AddColumn(new Column(name, align, rowAlign, minWidth, width, typeHandler));

    public Table AddColumn(Column column)
    {
        if (column is null)
        {
            throw new ArgumentNullException(nameof(column));
        }
        _columns.Add(column);
        return this;
    }

    public Table AddColumns(IEnumerable<Column> columns)
    {
        if (columns is null)
            throw new ArgumentNullException(nameof(columns));
        
        foreach (var column in columns)
        {
            AddColumn(column);
        }
        return this;
    }

    public Table AddColumns(IEnumerable<string> columns)
        => AddColumns(columns.Select(c => Column.FromName(c)));

    public Table AddColumns(params string[] columns)
        => AddColumns(columns.Select(c => Column.FromName(c)));

    public Table AddRow(params object?[] values)
        => AddRowImpl(new ValueRow(values));

    public Table AddRow(params string?[] values)
        => AddRowImpl(new ValueRow(values));

    public Table AddRow(Row row)
        => AddRowImpl(row);

    public Table AddRow<T>(T obj)
    {
        if (obj is not null)
        {
            return AddRowImpl(new ObjectRow(obj));
        }

        return this;
    }

    public Table AddRows<T>(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            AddRow(value);
        }

        return this;
    }

    private Table AddRowImpl(Row row)
    {
        if (row is null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        _rows.Add(row);
        return this;
    }
}