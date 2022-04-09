namespace TextTableBuilder.TypeHandlers;

public class NullHandler : INullValueHandler
{
    public static readonly INullValueHandler Default = new NullHandler();

    private readonly string _nullvalue;

    public NullHandler()
        : this(string.Empty) { }
    public NullHandler(string nullValue)
        => _nullvalue = nullValue;

    public string Handle(IFormatProvider formatProvider) => _nullvalue;
}