namespace TextTableBuilder.TypeHandlers;

public class LongHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((long)value).ToString(Format, formatProvider);
}
