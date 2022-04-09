namespace TextTableBuilder.TypeHandlers;

public class ShortHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((ushort)value).ToString(Format, formatProvider);
}
