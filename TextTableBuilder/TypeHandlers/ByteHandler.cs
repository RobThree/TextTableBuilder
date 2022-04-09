namespace TextTableBuilder.TypeHandlers;

public class ByteHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((byte)value).ToString(Format, formatProvider);
}
