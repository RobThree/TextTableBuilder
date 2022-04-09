namespace TextTableBuilder.TypeHandlers;

public class DoubleHandler : NaturalFractionalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((double)value).ToString(Format, formatProvider);
}
