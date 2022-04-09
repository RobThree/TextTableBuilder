namespace TextTableBuilder.TypeHandlers;

public class SByteHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((sbyte)value).ToString(Format, formatProvider);
}
