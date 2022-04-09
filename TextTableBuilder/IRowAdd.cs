namespace TextTableBuilder;

public interface IRowAdd
{
    IRowAdd AddRow(params object[] values);
    IRowAdd AddRow(ValueRow row);
    IRowAdd AddRow(ObjectRow row);
    IRowAdd AddRow<T>(T value);

    IRowAdd AddRows(IEnumerable<ValueRow> rows);
    IRowAdd AddRows(IEnumerable<ObjectRow> rows);
    IRowAdd AddRows<T>(IEnumerable<T> values);
}
