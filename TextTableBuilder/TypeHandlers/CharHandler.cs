namespace TextTableBuilder.TypeHandlers;

public class CharHandler : NaturalNumberHandler
{
    public override string Handle(object value, IFormatProvider formatProvider) => ((char)value).ToString(formatProvider);
}
