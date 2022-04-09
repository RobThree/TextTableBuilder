namespace TextTableBuilder.TypeHandlers;

public class UShortHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((ulong)value).ToString(Format, formatProvider);
}