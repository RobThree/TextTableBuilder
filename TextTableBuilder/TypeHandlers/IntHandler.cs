namespace TextTableBuilder.TypeHandlers;

public class IntHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((int)value).ToString(Format, formatProvider);
}