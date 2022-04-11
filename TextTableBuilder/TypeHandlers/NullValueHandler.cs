namespace TextTableBuilder.TypeHandlers;

public class NullValueHandler : INullValueHandler
{
    public static readonly INullValueHandler Default = new NullValueHandler();

    private readonly string _nullvalue;

    public NullValueHandler()
        : this(string.Empty) { }
    public NullValueHandler(string nullValue)
        => _nullvalue = nullValue;

    public string Handle(IFormatProvider formatProvider) => _nullvalue;
}