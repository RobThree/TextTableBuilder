﻿namespace TextTableBuilder.TypeHandlers;

public class TypeHandlerCollection : HandlerCollection<ITypeHandler>
{
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
        AddHandler<object>(new DefaultTypeHandler());
    }
}