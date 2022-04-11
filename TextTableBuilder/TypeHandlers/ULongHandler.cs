namespace TextTableBuilder.TypeHandlers;

public class ULongHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((ulong)value).ToString(Format, formatProvider);
}
