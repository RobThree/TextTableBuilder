namespace TextTableBuilder;

public interface IRowAdd
{
    IRowAdd AddRow(params object[] values);
    IRowAdd AddRow(ValueRow row);
    IRowAdd AddRow(ObjectRow row);
    IRowAdd AddRow<T>(T value);
}
