namespace TextTableBuilder.TypeHandlers;

public class UShortHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((ushort)value).ToString(Format, formatProvider);
}