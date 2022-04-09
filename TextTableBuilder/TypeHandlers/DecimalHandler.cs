namespace TextTableBuilder.TypeHandlers;

public class DecimalHandler : NaturalFractionalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((decimal)value).ToString(Format, formatProvider);
}
