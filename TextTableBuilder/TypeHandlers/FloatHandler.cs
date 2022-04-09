namespace TextTableBuilder.TypeHandlers;

public class FloatHandler : NaturalFractionalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((float)value).ToString(Format, formatProvider);
}
