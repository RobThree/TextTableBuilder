namespace TextTableBuilder.TypeHandlers;

public class TypeHandlerCollection : HandlerCollection<ITypeHandler>
{
    public INullValueHandler NullValueHandler { get; set; } = TypeHandlers.NullValueHandler.Default;

    public void AddHandler<T>(Func<T, IFormatProvider, string> func) => AddHandler<T>(new DelegatingTypeHandler<T>(func));

    public TypeHandlerCollection()
    {
        AddHandler<bool>(new BoolHandler());
        AddHandler<byte>(new ByteHandler());
        AddHandler<sbyte>(new SByteHandler());
        AddHandler<char>(new CharHandler());
        AddHandler<decimal>(new DecimalHandler());
        AddHandler<double>(new DoubleHandler());
        AddHandler<float>(new FloatHandler());
        AddHandler<int>(new IntHandler());
        AddHandler<uint>(new UIntHandler());
        AddHandler<long>(new LongHandler());
        AddHandler<ulong>(new ULongHandler());
        AddHandler<short>(new ShortHandler());
        AddHandler<ushort>(new UShortHandler());
        AddHandler<DateTime>(new DateTimeHandler());
        AddHandler<TimeSpan>(new TimeSpanHandler());
        AddHandler<Guid>(new GuidHandler());
        AddHandler<object>(new DefaultTypeHandler());
    }
}
